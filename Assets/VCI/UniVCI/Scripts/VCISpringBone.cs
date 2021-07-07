﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniGLTF;

namespace VCI
{
    /// <summary>
    /// The base algorithm is http://rocketjump.skr.jp/unity3d/109/ of @ricopin416
    /// DefaultExecutionOrder(11000) means calculate springbone after FinalIK( VRIK )
    /// </summary>
    [DefaultExecutionOrder(11000)]
    [RequireComponent(typeof(VCIObject))]
    public sealed class VCISpringBone : MonoBehaviour
    {
        [SerializeField] [Header("Gizmo")] private bool m_drawGizmo;

        [SerializeField] private Color m_gizmoColor = Color.yellow;

        [SerializeField] [Range(0, 4)] [Header("Settings")]
        public float m_stiffnessForce = 1.0f;

        [SerializeField] [Range(0, 2)] public float m_gravityPower;

        [SerializeField] public Vector3 m_gravityDir = new Vector3(0, -1.0f, 0);

        [SerializeField] [Range(0, 1)] public float m_dragForce = 0.4f;

        [SerializeField] public Transform m_center;
        
        [SerializeField] public List<Transform> RootBones = new List<Transform>();

        [SerializeField] [Range(0, 0.5f)] [Header("Collider")]
        public float m_hitRadius = 0.02f;

        [SerializeField] public List<Transform> m_colliderObjects;

        private List<SphereCollider> m_colliders = new List<SphereCollider>();

        private Dictionary<Transform, Quaternion> m_initialLocalRotationMap;

        private readonly List<VCISpringBoneLogic> m_verlet = new List<VCISpringBoneLogic>();


        private void Awake()
        {
            Setup();
        }

        [ContextMenu("Reset bones")]
        public void Setup(bool force = false)
        {
            if (RootBones != null)
            {
                if (force || m_initialLocalRotationMap == null)
                {
                    m_initialLocalRotationMap = new Dictionary<Transform, Quaternion>();
                }
                else
                {
                    foreach (var kv in m_initialLocalRotationMap) kv.Key.localRotation = kv.Value;
                    m_initialLocalRotationMap.Clear();
                }

                m_verlet.Clear();

                foreach (var go in RootBones)
                {
                    if (go != null)
                    {
                        foreach (var x in go.transform.Traverse()) m_initialLocalRotationMap[x] = x.localRotation;

                        SetupRecursive(m_center, go);
                    }
                }

                if (m_colliderObjects != null)
                {
                    foreach (var go in m_colliderObjects)
                    {
                        var colliders = go.GetComponents<SphereCollider>();
                        if (colliders != null && colliders.Length > 0)
                        {
                            m_colliders.AddRange(colliders);
                        }
                    }
                }
            }
        }

        public void SetLocalRotationsIdentity()
        {
            foreach (var verlet in m_verlet) verlet.Head.localRotation = Quaternion.identity;
        }

        private static IEnumerable<Transform> GetChildren(Transform parent)
        {
            for (var i = 0; i < parent.childCount; ++i) yield return parent.GetChild(i);
        }

        private void SetupRecursive(Transform center, Transform parent)
        {
            if (parent.childCount == 0)
            {
                var delta = parent.position - parent.parent.position;
                var childPosition = parent.position + delta.normalized * 0.07f;
                m_verlet.Add(new VCISpringBoneLogic(center, parent,
                    parent.worldToLocalMatrix.MultiplyPoint(childPosition)));
            }
            else
            {
                var firstChild = GetChildren(parent).First();
                var localPosition = firstChild.localPosition;
                var scale = firstChild.lossyScale;
                m_verlet.Add(new VCISpringBoneLogic(center, parent,
                        new Vector3(
                            localPosition.x * scale.x,
                            localPosition.y * scale.y,
                            localPosition.z * scale.z
                        )))
                    ;
            }

            foreach (Transform child in parent) SetupRecursive(center, child);
        }

        private void LateUpdate()
        {
            if (m_verlet == null || m_verlet.Count == 0)
            {
                if (RootBones == null) return;

                Setup();
            }

            var stiffness = m_stiffnessForce * Time.deltaTime;
            var external = m_gravityDir * (m_gravityPower * Time.deltaTime);

            foreach (var verlet in m_verlet)
            {
                verlet.Radius = m_hitRadius;
                verlet.Update(m_center,
                    stiffness,
                    m_dragForce,
                    external,
                    m_colliders
                );
            }
        }

        private void OnDrawGizmos()
        {
            if (!m_drawGizmo) return;

            foreach (var verlet in m_verlet)
                verlet.DrawGizmo(m_center, m_hitRadius, m_gizmoColor);
        }

        /// <summary>
        /// original from
        /// http://rocketjump.skr.jp/unity3d/109/
        /// </summary>
        private class VCISpringBoneLogic
        {
            Transform m_transform;
            public Transform Head => m_transform;

            private Vector3 m_boneAxis;
            private Vector3 m_currentTail;

            private readonly float m_length;
            private Vector3 m_localDir;
            private Vector3 m_prevTail;

            public VCISpringBoneLogic(Transform center, Transform transform, Vector3 localChildPosition)
            {
                m_transform = transform;
                var worldChildPosition = m_transform.TransformPoint(localChildPosition);
                m_currentTail = center != null
                        ? center.InverseTransformPoint(worldChildPosition)
                        : worldChildPosition;
                m_prevTail = m_currentTail;
                LocalRotation = transform.localRotation;
                m_boneAxis = localChildPosition.normalized;
                m_length = localChildPosition.magnitude;
            }

            public Vector3 Tail => m_transform.localToWorldMatrix.MultiplyPoint(m_boneAxis * m_length);

            private Quaternion LocalRotation { get; }

            public float Radius { get; set; }

            private Quaternion ParentRotation =>
                m_transform.parent != null
                    ? m_transform.parent.rotation
                    : Quaternion.identity;

            public void Update(Transform center,
                float stiffnessForce, float dragForce, Vector3 external,
                List<SphereCollider> colliders)
            {
                var currentTail = center != null
                        ? center.TransformPoint(m_currentTail)
                        : m_currentTail;
                var prevTail = center != null
                        ? center.TransformPoint(m_prevTail)
                        : m_prevTail;

                // verlet積分で次の位置を計算
                var nextTail = currentTail
                               + (currentTail - prevTail) * (1.0f - dragForce) // 前フレームの移動を継続する(減衰もあるよ)
                               + ParentRotation * LocalRotation * m_boneAxis * stiffnessForce // 親の回転による子ボーンの移動目標
                               + external; // 外力による移動量

                // 長さをboneLengthに強制
                var position = m_transform.position;
                nextTail = position + (nextTail - position).normalized * m_length;

                // Collisionで移動
                nextTail = Collision(colliders, nextTail);

                m_prevTail = center != null
                        ? center.InverseTransformPoint(currentTail)
                        : currentTail;

                m_currentTail = center != null
                        ? center.InverseTransformPoint(nextTail)
                        : nextTail;

                //回転を適用
                m_transform.rotation = ApplyRotation(nextTail);
            }

            protected virtual Quaternion ApplyRotation(Vector3 nextTail)
            {
                var rotation = ParentRotation * LocalRotation;
                return Quaternion.FromToRotation(rotation * m_boneAxis,
                           nextTail - m_transform.position) * rotation;
            }

            protected virtual Vector3 Collision(List<SphereCollider> colliders, Vector3 nextTail)
            {
                var position = m_transform.position;

                foreach (var collider in colliders)
                {
                    var ls = collider.transform.lossyScale;
                    var scale = Mathf.Max(ls.x, ls.y, ls.z);

                    var r = Radius + collider.radius * scale;
                    var colliderPosition = collider.transform.TransformPoint(collider.center);

                    if (Vector3.SqrMagnitude(nextTail - colliderPosition) > r * r) continue;

                    // ヒット。Colliderの半径方向に押し出す
                    var normal = (nextTail - colliderPosition).normalized;
                    var posFromCollider = colliderPosition + normal * (Radius + collider.radius * scale);

                    // 長さをboneLengthに強制
                    nextTail = position + (posFromCollider - position).normalized * m_length;
                }

                return nextTail;
            }

            public void DrawGizmo(Transform center, float radius, Color color)
            {
                var currentTail = center != null
                        ? center.TransformPoint(m_currentTail)
                        : m_currentTail;
                var prevTail = center != null
                        ? center.TransformPoint(m_prevTail)
                        : m_prevTail;

                Gizmos.color = Color.gray;
                Gizmos.DrawLine(currentTail, prevTail);
                Gizmos.DrawWireSphere(prevTail, radius);

                Gizmos.color = color;
                Gizmos.DrawLine(currentTail, m_transform.position);
                Gizmos.DrawWireSphere(currentTail, radius);
            }
        }
    }
}