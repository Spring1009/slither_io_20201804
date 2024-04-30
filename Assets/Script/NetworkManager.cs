using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System.Text;

public class NetworkManager : MonoBehaviour
{
    TcpClient client;
    NetworkStream netStream;
    // Start is called before the first frame update
    void Start()
    {
        client = new TcpClient("127.0.0.1", 8001);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            netStream = client.GetStream();
            var bw = new BinaryWriter(netStream, Encoding.Default, true);

            bw.Write("Test");
        }
    }
}
