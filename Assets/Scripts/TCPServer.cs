using System;
using System.Collections; 
using System.Collections.Generic; 
using System.Net; 
using System.Net.Sockets; 
using System.Text; 
using System.Threading; 
using UnityEngine;  

public class TCPServer : MonoBehaviour {  	
	#region private members 	
	/// <summary> 	
	/// TCPListener to listen for incomming TCP connection 	
	/// requests. 	
	/// </summary> 	
	private TcpListener tcpListener; 
	/// <summary> 
	/// Background thread for TcpServer workload. 	
	/// </summary> 	
	private Thread tcpListenerThread;  	
	/// <summary> 	
	/// Create handle to connected tcp client. 	
	/// </summary> 	
	private TcpClient connectedTcpClient; 	
	#endregion 	
		
	// Use this for initialization
	void Start () { 		
		// Start TcpServer background thread 		
		tcpListenerThread = new Thread (new ThreadStart(ListenForIncommingRequests)); 		
		tcpListenerThread.IsBackground = true; 		
		tcpListenerThread.Start(); 	
	}  	
	
	// Update is called once per frame
	void Update () { 		
		//if (Input.GetKeyDown(KeyCode.Space)) {             
		//	SendMessage();         
		//} 	
	}  	
	
	/// <summary> 	
	/// Runs in background TcpServerThread; Handles incomming TcpClient requests 	
	/// </summary> 	
	private void ListenForIncommingRequests () { 		
		try { 			
			// Create listener on localhost port 8052. 			
			tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8052); 			
			tcpListener.Start();              
			Debug.Log("Server is listening");              
			Byte[] bytes = new Byte[1024];  			
			while (true) { 				
				using (connectedTcpClient = tcpListener.AcceptTcpClient()) {
					Debug.Log("Got connection");
					// Get a stream object for reading 					
					using (NetworkStream stream = connectedTcpClient.GetStream()) {
						while (true)
						{
							byte[] lenBuf = new byte[1];
							int jsonLength = 0;
							bool foundLength = false;
							while (!foundLength)
							{
								lenBuf[0] = (byte)'#';
								stream.Read(lenBuf, 0, lenBuf.Length);
								if (lenBuf[0] == '#')
								{
									foundLength = true;
								}
								else
								{
									jsonLength *= 10;
									jsonLength += Int32.Parse(Encoding.UTF8.GetString(lenBuf));
								}
							}

							if (jsonLength == 0)
							{
								Debug.LogError("Corrupt stream");
								break;
							}

							int totalBytesRead = 0;
							byte[] buffer = new byte[jsonLength];
							bool readSuccess = true;
							int bytesRemaining = jsonLength;

							while (bytesRemaining > 0)
							{
								int bytesRead = stream.Read(buffer, totalBytesRead, bytesRemaining);
								totalBytesRead += bytesRead;
								bytesRemaining -= bytesRead;
								if (bytesRead <= 0)
								{
									Debug.LogError("Failed to read from stream");
									readSuccess = false;
									break;
								}
							}

							if (!readSuccess)
							{
								Debug.LogError("Corrupt stream");
								break;
							}

							string dataString = Encoding.UTF8.GetString(buffer);
							UnityMessage message = JsonUtility.FromJson<UnityMessage>(dataString);
							GameManager.OnMessage(message);
						}
					} 				
				} 			
			} 		
		} 		
		catch (SocketException socketException) { 			
			Debug.Log("SocketException " + socketException.ToString()); 		
		}     
	}  	
	/// <summary> 	
	/// Send message to client using socket connection. 	
	/// </summary> 	
	//private void SendMessage() { 		
	//	if (connectedTcpClient == null) {             
	//		return;         
	//	}  		
		
	//	try { 			
	//		// Get a stream object for writing. 			
	//		NetworkStream stream = connectedTcpClient.GetStream(); 			
	//		if (stream.CanWrite) {                 
	//			string serverMessage = "This is a message from your server."; 			
	//			// Convert string message to byte array.                 
	//			byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(serverMessage); 				
	//			// Write byte array to socketConnection stream.               
	//			stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);               
	//			Debug.Log("Server sent his message - should be received by client");           
	//		}       
	//	} 		
	//	catch (SocketException socketException) {             
	//		Debug.Log("Socket exception: " + socketException);         
	//	} 	
	//} 
}