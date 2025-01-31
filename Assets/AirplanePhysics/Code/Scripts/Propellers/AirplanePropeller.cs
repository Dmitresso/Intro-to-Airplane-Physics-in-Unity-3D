﻿using UnityEngine;


namespace WheelApps {
    public class AirplanePropeller : MonoBehaviour {
        #region Variables
        [Header("Propeller Properties")]
        public float minRotationRPM = 30f;
        public float minQuadRPMs = 300f;
        public float minTextureSwap = 600f;
        public GameObject mainProp;
        public GameObject blurredProp;
        
        [Header("Material Properties")]
        public Material blurredPropMat;
        public Texture2D blurLevel1; 
        public Texture2D blurLevel2;
        #endregion


        
        #region Constants
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        #endregion



        #region Builtin Methods
        private void Start() {
            if (mainProp && blurredProp) HandleSwapping(0f);            
        }
        #endregion



        #region Custom Methods
        public void HandlePropeller(float currentRPM) {
            // degrees per second
            var dps = (currentRPM * 360f / 60f + minRotationRPM) * Time.deltaTime;
            dps = Mathf.Clamp(dps, 0f, minRotationRPM);
            transform.Rotate(Vector3.forward, dps);

            if (mainProp && blurredProp) HandleSwapping(currentRPM);
        }

        
        private void HandleSwapping(float currentRPM) {
            if (currentRPM > minQuadRPMs) {
                blurredProp.gameObject.SetActive(true);
                mainProp.gameObject.SetActive(false);

                if (blurredPropMat && blurLevel1 && blurLevel2) {
                    blurredPropMat.SetTexture(MainTex, currentRPM > minTextureSwap ? blurLevel2 : blurLevel1);
                }
            }
            else {
                blurredProp.gameObject.SetActive(false);
                mainProp.gameObject.SetActive(true);
            }
        }
        #endregion
    }
}