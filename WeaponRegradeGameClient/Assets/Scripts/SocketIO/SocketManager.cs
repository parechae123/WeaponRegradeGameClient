using System.Collections;
using WebSocketSharp;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Text;
using UnityEngine.UI;

[System.Serializable]
public class MyData
{
    public string clientID;
    public string userID;
    public string regradeLevel;
    public string message;
    public int requestType;
}

public class InfoData
{
    public string type;
    public InfoParams myParams = new InfoParams();
}

public class InfoParams
{
    public string room;
    public int loopTimeCount;
}

public class SocketManager : MonoBehaviour
{
    private static SocketManager instance;
    public static SocketManager Instance { get { return instance; } }

    private WebSocket webSocket;
    private bool IsConnected = false;
    private int ConnectionAttempt = 0;
    private const int MaxConnectionAttempts = 3;

    private Queue<MyData> broadcastingText = new Queue<MyData>();
    private float timer = 2;
    private float resetTime = 2;

    public Text text;
    public InputField inputField;

    public int loopCount;

    [SerializeField]MyData sendData = new MyData { message = "메세지 보내기" };
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            ConnectWebSock();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    void ConnectWebSock()
    {
        webSocket = new WebSocket("ws://localhost:8000");   //8000vhxmdp dusruf
        webSocket.OnOpen += OnWebSocketOpen;            //함수 이벤트 등록
        webSocket.OnMessage += OnWebSocketMessage;      //함수 이벤트 등록
        webSocket.OnClose += OnWebSocketClose;          //함수 이벤트 등록

        webSocket.ConnectAsync();
    }

    void OnWebSocketOpen(object sender, System.EventArgs e)
    {
        Debug.Log("WebSocket connected");
        IsConnected = true;
        ConnectionAttempt = 0;
    }
    void OnWebSocketMessage(object sender, MessageEventArgs e)
    {
        string jsonData = Encoding.Default.GetString(e.RawData);
        Debug.Log("Received JSON data : " + jsonData);
        Debug.Log("리시브 데이터 : ");
        //text.text = jsonData;

        //Json 데이터를 객체로 역직렬화
        MyData receivedData = JsonConvert.DeserializeObject<MyData>(jsonData);

        InfoData infoData = JsonConvert.DeserializeObject<InfoData>(jsonData);
        if (infoData != null)
        {
            string room = infoData.myParams.room;
            loopCount = infoData.myParams.loopTimeCount;
        }

        if (receivedData != null && !string.IsNullOrEmpty(receivedData.clientID))
        {
            sendData.clientID = receivedData.clientID;
        }
        if (receivedData.requestType == 20)
        {
            broadcastingText.Enqueue(JsonConvert.DeserializeObject<MyData>(jsonData));
        }
    }
    void OnWebSocketClose(object sender, CloseEventArgs e)
    {
        Debug.Log("WebSocket connected Closed");
        IsConnected = false;

        if (ConnectionAttempt < MaxConnectionAttempts)
        {
            ConnectionAttempt++;
            Debug.Log("Attempting to reconnect . Attemp : " + ConnectionAttempt);
            ConnectWebSock();
        }
        else
        {
            Debug.Log("Failed to connect ager " + MaxConnectionAttempts + "attempts. ");
        }
    }

    private void OnApplicationQuit()            //어플 종료되면 소켓 제거
    {
        DisconnectWebSocket();
    }

    void DisconnectWebSocket()
    {
        if (webSocket != null && IsConnected)
        {
            webSocket.Close();
            IsConnected = false;
        }
    }
    public void newRecordBreak()
    {
        if (GameManager.Instance.PlayerInven.WeaponIndex > GameManager.Instance.PlayerInven.maxRegrade)
        {
            sendData.requestType = 20;
            sendData.regradeLevel = GameManager.Instance.PlayerInven.WeaponIndex.ToString();
            sendData.userID = GameManager.Instance.PlayerInven.userID;
            string jsonData = JsonConvert.SerializeObject(sendData);

            webSocket.Send(jsonData);

        }
    }

    public void SendSocketMessage()
    {
        sendData.requestType = 0;
        sendData.message = inputField.text;
        string jsonData = JsonConvert.SerializeObject(sendData);

        webSocket.Send(jsonData);
    }

    private void Update()
    {
        if (webSocket == null || !IsConnected) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Json 데이터 생성
            sendData.requestType = 0;
            string jsonData = JsonConvert.SerializeObject(sendData);

            webSocket.Send(jsonData);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            newRecordBreak();
        }
        if (timer<= 0)
        {
            if (broadcastingText.Count>0)
            {
                MyData tempData = broadcastingText.Dequeue();
                text.text = tempData.userID+"님이 "+tempData.regradeLevel+"강화에 성공하셨습니다!!";
            }
            else
            {
                text.text = string.Empty;
            }
            timer = resetTime;
        }
        timer -= Time.deltaTime;
    }


}
