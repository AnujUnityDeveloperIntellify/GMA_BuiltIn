using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpeedVariation
{
    public class GPUTest : MonoBehaviour
    {

        [SerializeField] private List<GameObject> PrefabList;
        [SerializeField] private Transform Pos1;
        [SerializeField] private Transform Pos2;
        [SerializeField] private Transform Pos3;
        [Space(10)]
        [SerializeField] private Sprite sp1;
        [SerializeField] private Sprite sp2;
        [SerializeField] private Sprite sp3;
        private Vector3 SpwanPos = Vector3.zero;
        private float zincrement = 1.5f;
        private float sideincrement = -1.5f;
        private Vector3 roat = new Vector3(90, 0, 0);

        /*[ContextMenu("T1")]
        private void ChangeMatAlbedoT1()
        {
            MeshRenderer currentTrophy = trophy1;
            if (currentTrophy == null ) return;

            MaterialPropertyBlock mpb = new MaterialPropertyBlock();
            currentTrophy.GetPropertyBlock(mpb);

            mpb.SetTexture("_BaseMap", sp1.texture); 
            currentTrophy.SetPropertyBlock(mpb);
        }

        [ContextMenu("T2")]
        private void ChangeMatAlbedoT2()
        {
            MeshRenderer currentTrophy = trophy2;
            if (currentTrophy == null ) return;

            MaterialPropertyBlock mpb = new MaterialPropertyBlock();
            currentTrophy.GetPropertyBlock(mpb);

            mpb.SetTexture("_BaseMap", sp2.texture);
            currentTrophy.SetPropertyBlock(mpb);
        }
        [ContextMenu("T3")]
        private void ChangeMatAlbedoT3()
        {
            MeshRenderer currentTrophy = trophy3;
            if (currentTrophy == null ) return;

            MaterialPropertyBlock mpb = new MaterialPropertyBlock();
            currentTrophy.GetPropertyBlock(mpb);

            mpb.SetTexture("_BaseMap", sp3.texture);
            currentTrophy.SetPropertyBlock(mpb);
        }*/

        [ContextMenu("Inst")]
        private void DoInst()
        {
            StartCoroutine(startSpwan());
        }
        private IEnumerator startSpwan()
        {
            for (float z = -4; z < 5; z += zincrement)
            {
                for (float x = 7; x > -8; x += sideincrement)
                {
                    SpwanPos = new Vector3(x, 1, z);
                    SpwanObject(SpwanPos);
                    yield return new WaitForSeconds(1f);
                }
            }
        }
        private void SpwanObject(Vector3 pos)
        {
            Instantiate(PrefabList[UnityEngine.Random.Range(0,PrefabList.Count)], pos, Quaternion.Euler(90,0,0));
        }
    }
}

