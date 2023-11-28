using UnityEngine;
using WebSocketSharp;

public class WebSocketExample : MonoBehaviour
{
    private WebSocket ws;

    void Start()
    {
        // 웹소켓 클라이언트 생성 및 연결 시도
        ws = new WebSocket("wss://port-0-weaponregradewebsocket-12fhqa2bln15fvi3.sel5.cloudtype.app"); // 서버의 URL 및 포트 번호로 변경

        // 연결 시 이벤트 처리
        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("서버에 연결되었습니다.");
            ws.Send("안녕하세요, 서버!");
        };

        // 메시지 수신 시 이벤트 처리
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("서버로부터 메시지 받음: " + e.Data);
        };

        // 연결 종료 시 이벤트 처리
        ws.OnClose += (sender, e) =>
        {
            Debug.Log("서버와의 연결이 종료되었습니다.");
        };

        // 연결 시도
        ws.Connect();
    }

    void OnDestroy()
    {
        if (ws != null && ws.IsAlive)
        {
            ws.Close();
        }
    }
}
