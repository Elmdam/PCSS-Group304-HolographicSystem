using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;



public class NetworkScript : MonoBehaviour {
    Client newClient;


    void Start () {

         newClient = new Client(1234);


    }
	

	void Update () {

        if (Input.GetKeyUp("a"))
        {
            newClient.UpdateServer();
        }

    }

    class Client
    {
        NetworkStream stream;
        StreamReader reader;
        StreamWriter writer;
        TcpClient client;
        int port;
        float[,] dataArray;


        public Client(int _port)
        {
            dataArray = new float[4, 2];
            port = _port;
            client = new TcpClient("localhost", port);
            stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
        
        }

        void UpdatePlayers()
        {
            GameObject playerOne = GameObject.FindGameObjectWithTag("PlayerOne");
            GameObject playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");
            GameObject playerThree = GameObject.FindGameObjectWithTag("PlayerThree");
            GameObject playerFour = GameObject.FindGameObjectWithTag("PlayerFour");

            dataArray[0, 0] = playerOne.transform.position.x;
            dataArray[0, 1] = playerOne.transform.position.y;

            dataArray[1, 0] = playerTwo.transform.position.x;
            dataArray[1, 1] = playerTwo.transform.position.y;

            dataArray[2, 0] = playerThree.transform.position.x;
            dataArray[2, 1] = playerThree.transform.position.y;

            dataArray[3, 0] = playerFour.transform.position.x;
            dataArray[3, 1] = playerFour.transform.position.y;
        }

        public void UpdateServer()
        {
            UpdatePlayers();
            for (int i = 0; i < dataArray.GetLength(0); i++)
            {
                for (int j = 0; j < dataArray.GetLength(1); j++)
                {
                    string info = Convert.ToString(dataArray[i, j]);
                    writer.WriteLine(info);
                    
                }
            }
        }
        public void ReceiveUpdate()
        {

        }
    }
}
