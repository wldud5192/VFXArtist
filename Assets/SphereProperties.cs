using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereProperties : MonoBehaviour
{

    //To instantiate the gameObject
    public GameObject playerSphere;
    public Transform playerSpawn;
    public GameObject clientSphere;
    public Transform clientSpawn;

    //To manually assign textures in the inspector
    public Texture[] textures;
    public Texture[] normals;

    //To get the selected values from UI
    public Slider ui_voronoi_Cell_Density;
    public Slider ui_voronoi_Offset_Speed;
    public Slider ui_normal_strength;
    public Dropdown ui_texturePicker;
    public Dropdown ui_normalPicker;

    //To select the gameObject
    Material playerMat;
    Material clientMat;

    //To compare Player vs Client
    public int[] playerMatProp;
    public int[] clientMatProp;
    public bool[] matchProp;

    //To show how similar the player's sphere is
    Text matchSimilarity;
    public int matchSimilarityNum;

    void Start()
    {
        CreateNewPlayerSphere();
        CreateNewClientSphere();

        //To compare Player vs Client
        playerMatProp = new int[5];
        clientMatProp = new int[5];
        matchProp = new bool[5];

        matchSimilarity = GameObject.FindGameObjectWithTag("Similarity").GetComponent<Text>();

        clientMatProp[0] = Random.Range(0, 15);
        clientMatProp[1] = Random.Range(0, 5);
        clientMatProp[2] = Random.Range(0, 50);
        clientMatProp[3] = Random.Range(0, 5);
        clientMatProp[4] = Random.Range(0, 5);
        

        clientMat.SetTexture("_Texture_Map", textures[clientMatProp[3]]);
        clientMat.SetTexture("_Normal_Map", textures[clientMatProp[4]]);
        clientMat.SetFloat("_Cell_Density", clientMatProp[0]);
        clientMat.SetFloat("_Offset_Speed", clientMatProp[1]);
        clientMat.SetFloat("_Normal_Strength", clientMatProp[2]);

    }

    void Update()
    {
        //[0] : Cell density
        //[1] : Offset speed
        //[2] : Strength
        //[3] : Texture
        //[4] : Normal
        playerMatProp[0] = Mathf.RoundToInt(ui_voronoi_Cell_Density.value);
        playerMatProp[1] = Mathf.RoundToInt(ui_voronoi_Offset_Speed.value);
        playerMatProp[2] = Mathf.RoundToInt(ui_normal_strength.value);
        playerMatProp[3] = ui_texturePicker.value;
        playerMatProp[4] = ui_normalPicker.value;


        //To check if Player has the same value as the client
        //1 to 5: Texture Map, Normal map, Offset Speed
        //1 to 15: Cell Density
        //1 to 50: Normal Strength
        CompareThem();

        matchSimilarityNum = CheckMatching.CountTrue(matchProp, true);
    }

    public void CompareThem()
    {
        //To check if Player has the same value as the client
        //1 to 5: Texture Map, Normal map, Offset Speed
        //1 to 15: Cell Density
        //1 to 50: Normal Strength

        for (int i = 0; i < playerMatProp.Length; i++)
        {
            if (playerMatProp[i] != clientMatProp[i])
            {
                matchProp[i] = false;
            }
            else
            {
                matchProp[i] = true;
                matchSimilarity.text = matchSimilarityNum.ToString("0");
            }
        }            
    }
    

    public void ChangeTextureMap()
    {
        playerMat.SetTexture("_Texture_Map", textures[ui_texturePicker.value]);
    }

    public void ChangeNormalMap()
    {
        playerMat.SetTexture("_Normal_Map", textures[ui_normalPicker.value]);
    }

    public void VoronoiCellDensity()
    {
        playerMat.SetFloat("_Cell_Density", playerMatProp[0]);
    }

    public void VoronoiOffsetSpeed()
    {
        playerMat.SetFloat("_Offset_Speed", playerMatProp[1]);
    }

    public void NormalStrength()
    {
        playerMat.SetFloat("_Normal_Strength", playerMatProp[2]);
    }

    public void CreateNewPlayerSphere()
    {
        if(GameObject.FindGameObjectWithTag("Player"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            Instantiate(playerSphere, playerSpawn.position, Quaternion.identity);
            playerMat = playerSphere.GetComponent<Renderer>().sharedMaterial;

            playerMat.SetTexture("_Texture_Map", textures[0]);
            playerMat.SetTexture("_Normal_Map", textures[0]);
            playerMat.SetFloat("_Cell_Density", 0);
            playerMat.SetFloat("_Offset_Speed", 0);
            playerMat.SetFloat("_Normal_Strength", 0);
            playerMatProp = new int[5];
        } else
        {
            Instantiate(playerSphere, playerSpawn.position, Quaternion.identity);
            playerMat = playerSphere.GetComponent<Renderer>().sharedMaterial;
            playerMat.SetTexture("_Texture_Map", textures[0]);
            playerMat.SetTexture("_Normal_Map", textures[0]);
            playerMat.SetFloat("_Cell_Density", 0);
            playerMat.SetFloat("_Offset_Speed", 0);
            playerMat.SetFloat("_Normal_Strength", 0);
            playerMatProp = new int[5];
        }

    }

    public void CreateNewClientSphere()
    {

        //[0] : Cell density
        //[1] : Offset speed
        //[2] : Strength
        //[3] : Texture
        //[4] : Normal

        if (GameObject.FindGameObjectWithTag("Client"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Client"));

            clientMatProp[0] = Random.Range(0, 15);
            clientMatProp[1] = Random.Range(0, 5);
            clientMatProp[2] = Random.Range(0, 50);
            clientMatProp[3] = Random.Range(0, 5);
            clientMatProp[4] = Random.Range(0, 5);

            Instantiate(clientSphere, clientSpawn.position, Quaternion.identity);
            clientMat = clientSphere.GetComponent<Renderer>().sharedMaterial;

            clientMat.SetTexture("_Texture_Map", textures[clientMatProp[3]]);
            clientMat.SetTexture("_Normal_Map", textures[clientMatProp[4]]);
            clientMat.SetFloat("_Cell_Density", clientMatProp[0]);
            clientMat.SetFloat("_Offset_Speed", clientMatProp[1]);
            clientMat.SetFloat("_Normal_Strength", clientMatProp[2]);

        }
        else
        {
            clientMatProp[0] = Random.Range(0, 15);
            clientMatProp[1] = Random.Range(0, 5);
            clientMatProp[2] = Random.Range(0, 50);
            clientMatProp[3] = Random.Range(0, 5);
            clientMatProp[4] = Random.Range(0, 5);

            Instantiate(clientSphere, clientSpawn.position, Quaternion.identity);
            clientMat = clientSphere.GetComponent<Renderer>().sharedMaterial;

            clientMat.SetTexture("_Texture_Map", textures[clientMatProp[3]]);
            clientMat.SetTexture("_Normal_Map", textures[clientMatProp[4]]);
            clientMat.SetFloat("_Cell_Density", clientMatProp[0]);
            clientMat.SetFloat("_Offset_Speed", clientMatProp[1]);
            clientMat.SetFloat("_Normal_Strength", clientMatProp[2]);
        }

    }


}
