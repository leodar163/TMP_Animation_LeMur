using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TMP_Animation
{
    public class EffetsTMP
    {
        #region Effacement
        /// <summary>
        /// Appliquer une transparence au texte d'un TextMeshPro
        /// </summary>
        /// <param name="alpha">La valeur de transparence compris entre 0 et 255</param>
        /// <param name="texte">La référence au texte auquel on va appliquer la transparence</param>
        public static void EffacerTexte(byte alpha, TextMeshProUGUI texte)
        {
            for (int i = 0; i < texte.textInfo.wordCount; i++)
            {
                EffacerMot(alpha, i, texte);
            }
        }
    
        /// <summary>
        /// Appliquer une transparence à un mot dans le texte d'un TextMeshPro
        /// </summary>
        /// <param name="alpha">La valeur de transparence compris entre 0 et 255</param>
        /// <param name="indexMot">L'index du mot dans le texte du TextMeshPro</param>
        /// <param name="texte">La référence au texte auquel on va appliquer la transparence</param>
        public static void EffacerMot(byte alpha, int indexMot, TextMeshProUGUI texte)
        {
            TMP_WordInfo infos = texte.textInfo.wordInfo[indexMot];
            for (int i = 0; i < infos.characterCount; i++)
            {
                EffacerChar(alpha, infos.firstCharacterIndex + i, texte);
            }
        }
        /// <summary>
        /// Appliquer une transparence à un mot dans le texte d'un TextMeshPro
        /// </summary>
        /// <param name="alpha">La valeur de transparence compris entre 0 et 255</param>
        /// <param name="indexCar">L'index du caractère dans le texte du TextMeshPro</param>
        /// <param name="texte">La référence au texte auquel on va appliquer la transparence</param>
        public static void EffacerChar(byte alpha, int indexCar, TextMeshProUGUI texte)
        {
            int indexMesh = texte.textInfo.characterInfo[indexCar].materialReferenceIndex;
            int vertexIndex = texte.textInfo.characterInfo[indexCar].vertexIndex;
        
            //Pour éviter que les espaces blanc soient interprétés comme le premier charactère
            if(indexCar != 0 && vertexIndex == 0) return;
        
            Color32[] couleursVertex = texte.textInfo.meshInfo[indexMesh].colors32;

            Color32 couleur = couleursVertex[vertexIndex];
            couleur.a = alpha;
            
            couleursVertex[vertexIndex + 0] = couleur;
            couleursVertex[vertexIndex + 1] = couleur;
            couleursVertex[vertexIndex + 2] = couleur;
            couleursVertex[vertexIndex + 3] = couleur;
            texte.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
        }

        #endregion
        
        #region Coloration
        /// <summary>
        /// Applique une couleur au texte d'un TextMeshPro (ignore l'alpha)
        /// </summary>
        /// <param name="couleur">Couleur appliquée</param>
        /// <param name="texte">La référence au texte auquel on va appliquer la couleur</param>
        public static void ColorerTexte(Color32 couleur, TextMeshProUGUI texte)
        {
            for (int i = 0; i < texte.textInfo.wordCount; i++)
            {
                ColorerMot(couleur, i, texte);
            }
        }
    
        /// <summary>
        /// Applique une couleur à un mot du texte d'un TextMeshPro (ignore l'alpha)
        /// </summary>
        /// <param name="couleur">Couleur appliquée</param>
        /// <param name="indexMot">Index du mot dans le texte du TextMeshPro auquel on va appliquer la couleur</param>
        /// <param name="texte">La référence au texte auquel on va appliquer la couleur</param>
        public static void ColorerMot(Color32 couleur, int indexMot, TextMeshProUGUI texte)
        {
            TMP_WordInfo infos = texte.textInfo.wordInfo[indexMot];
            for (int i = 0; i < infos.characterCount; i++)
            {
                ColorerCar(couleur, infos.firstCharacterIndex + i, texte);
            }
        }
    
        /// <summary>
        /// Applique une couleur à un mot du texte d'un TextMeshPro (ignore l'alpha)
        /// </summary>
        /// <param name="couleur">Couleur appliquée</param>
        /// <param name="indexCar">Index du caractère dans le texte du TextMeshPro auquel on va appliquer la couleur</param>
        /// <param name="texte">La référence au texte auquel on va appliquer la couleur</param>
        public static void ColorerCar(Color32 couleur, int indexCar, TextMeshProUGUI texte)
        {
            int indexMesh = texte.textInfo.characterInfo[indexCar].materialReferenceIndex;
            int vertexIndex = texte.textInfo.characterInfo[indexCar].vertexIndex;
        
            //Pour éviter que les espaces blanc soient interprétés comme le premier charactère
            if(indexCar != 0 && vertexIndex == 0) return;
        
            Color32[] couleursVertex = texte.textInfo.meshInfo[indexMesh].colors32;

            couleur.a = couleursVertex[vertexIndex].a;
            
            couleursVertex[vertexIndex + 0] = couleur;
            couleursVertex[vertexIndex + 1] = couleur;
            couleursVertex[vertexIndex + 2] = couleur;
            couleursVertex[vertexIndex + 3] = couleur;
            texte.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
        }

        #endregion

        #region Attaque de Couleur
        /// <summary>
        /// Applique une couleur au texte d'un TextMeshPro, puis reviens progressivement à la couleur d'origine
        /// </summary>
        /// <param name="couleurSaute">Couleur du texte au début du saute</param>
        /// <param name="duree">Temps que met le texte à retrouver sa couleur initiale</param>
        /// <param name="texte">La référence au texte auquel on va appliquer le saute</param>
        public static void SauteDeCouleur(Color32 couleurSaute, float duree, TextMeshProUGUI texte)
        {
            for (int i = 0; i < texte.textInfo.wordCount; i++)
            {
                AttaqueDeCouleurSurMot(couleurSaute, i, duree, texte);
            }
        }
    
        /// <summary>
        /// Applique une couleur à un mot du texte d'un TextMeshPro, puis reviens progressivement à la couleur d'origine
        /// </summary>
        /// <param name="couleurSaute">Couleur du mot au début du saute</param>
        /// <param name="indexMot">L'index du mot dans le texte du TextMeshPro</param>
        /// <param name="duree">Temps que met le mot à retrouver sa couleur initiale</param>
        /// <param name="texte">La référence au texte auquel on va appliquer le saute</param>
        public static void AttaqueDeCouleurSurMot(Color32 couleurSaute, int indexMot, float duree, TextMeshProUGUI texte)
        {
            TMP_WordInfo info = texte.textInfo.wordInfo[indexMot];

            for (int i = 0; i < info.characterCount; i++)
            {
                AttaqueDeCouleurSurCar(couleurSaute, info.firstCharacterIndex + i, duree, texte);
            }
        }
        /// <summary>
        /// Applique une couleur à un caractère du texte d'un TextMeshPro, puis reviens progressivement à la couleur d'origine
        /// </summary>
        /// <param name="couleurSaute">Couleur du caractère au début du saute</param>
        /// <param name="indexCar">L'index du caractère dans le texte du TextMeshPro</param>
        /// <param name="duree">Temps que met le caractère à retrouver sa couleur initiale</param>
        /// <param name="texte">La référence au texte auquel on va appliquer le saute</param>
        public static void AttaqueDeCouleurSurCar(Color32 couleurSaute, int indexCar, float duree, TextMeshProUGUI texte)
        {
            int indexMesh = texte.textInfo.characterInfo[indexCar].materialReferenceIndex;
            int vertexIndex = texte.textInfo.characterInfo[indexCar].vertexIndex;
            Color32[] couleursVertex = texte.textInfo.meshInfo[indexMesh].colors32;
            Color32 couleurDepart = couleursVertex[vertexIndex];

            texte.StartCoroutine(ColorerCar(couleurSaute, couleurDepart, couleursVertex, vertexIndex, duree, texte));
        }
    
        private static IEnumerator ColorerCar(Color32 couleur, Color32 couleurDepart,Color32[] vertexColors, int vertexIndex,float duree, TextMeshProUGUI texte)
        {
            vertexColors[vertexIndex + 0] = couleur;
            vertexColors[vertexIndex + 1] = couleur;
            vertexColors[vertexIndex + 2] = couleur;
            vertexColors[vertexIndex + 3] = couleur;
            texte.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
        
            float tmps = 0;
            while (tmps <= duree)
            {
                Color32 couleurPas = Color32.Lerp(couleur, couleurDepart, tmps / duree);
                vertexColors[vertexIndex + 0] = couleurPas;
                vertexColors[vertexIndex + 1] = couleurPas;
                vertexColors[vertexIndex + 2] = couleurPas;
                vertexColors[vertexIndex + 3] = couleurPas;
            
                texte.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
                yield return new WaitForEndOfFrame();
                tmps += Time.unscaledDeltaTime;
            }
        }

        #endregion

        #region Tremblement

        /// <summary>
        /// Fait trembler le texte d'un TextMeshPro
        /// </summary>
        /// <param name="duree">Durée pendant laquelle on fait trembler le texte</param>
        /// <param name="force">Amplitude du tremblement</param>
        /// <param name="texte">Référence au TexteMeshPro dont on va modifier le texte</param>
        public static void FaireTremblerTexte(float duree, float force, TextMeshProUGUI texte)
        {
            for (int i = 0; i < texte.textInfo.wordCount; i++)
            {
                FaireTremblerMot(i, duree, force, texte);
            }
        }
        /// <summary>
        /// Fait trembler un mot du texte d'un TextMeshPro
        /// </summary>
        /// <param name="indexMot">Index du mot dans le texte du TextMeshPro</param>
        /// <param name="duree">Durée pendant laquelle on fait trembler le mot</param>
        /// <param name="force">Amplitude du tremblement</param>
        /// <param name="texte">Référence au TexteMeshPro dont on va modifier un mot</param>
        public static void FaireTremblerMot(int indexMot, float duree, float force, TextMeshProUGUI texte)
        {
            TMP_WordInfo info = texte.textInfo.wordInfo[indexMot];
        
            for (int i = 0; i < info.characterCount; i++)
            {
                FaireTremblerCar(i + info.firstCharacterIndex, duree, force, texte);
            }
        }
        /// <summary>
        /// Fait trembler un caractère du texte d'un TextMeshPro
        /// </summary>
        /// <param name="indexCar">Index du caractère dans le texte du TextMeshPro</param>
        /// <param name="duree">Durée pendant laquelle on fait trembler le caractère</param>
        /// <param name="force">Amplitude du tremblement</param>
        /// <param name="texte">Référence au TexteMeshPro dont on va modifier un caractère</param>
        public static void FaireTremblerCar(int indexCar, float duree, float force, TextMeshProUGUI texte)
        {
            int indexMesh = texte.textInfo.characterInfo[indexCar].materialReferenceIndex;
            int indexVertex = texte.textInfo.characterInfo[indexCar].vertexIndex;
            Vector3[] vertices = texte.textInfo.meshInfo[indexMesh].vertices;
            texte.StartCoroutine(TremblerCar(indexVertex, vertices, duree, force, texte));
        }
    
        private static IEnumerator TremblerCar(int indexVertex, Vector3[] vertices, float duree, float force, TextMeshProUGUI texte)
        {
            float tmps = 0;
            List<Vector3> positionDepart = new List<Vector3>(vertices);
            while (tmps <= duree)
            {
                Vector3 decalage = new Vector3(Random.Range(-1f,1), Random.Range(-1f,1)) * force;
            
                vertices[indexVertex] = positionDepart[indexVertex] + decalage;
                vertices[indexVertex + 1] = positionDepart[indexVertex + 1] + decalage;
                vertices[indexVertex + 2] = positionDepart[indexVertex + 2] + decalage;
                vertices[indexVertex + 3] = positionDepart[indexVertex + 3] + decalage;
            
                texte.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
            
                yield return new WaitForEndOfFrame();
                tmps += Time.unscaledDeltaTime;
            }
        
            vertices[indexVertex] = positionDepart[indexVertex];
            vertices[indexVertex + 1] = positionDepart[indexVertex + 1];
            vertices[indexVertex + 2] = positionDepart[indexVertex + 2];
            vertices[indexVertex + 3] = positionDepart[indexVertex + 3];
            
            texte.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
        }

        #endregion
    }
}
