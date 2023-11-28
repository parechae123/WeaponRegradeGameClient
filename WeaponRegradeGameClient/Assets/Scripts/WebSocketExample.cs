using UnityEngine;
using WebSocketSharp;

public class WebSocketExample : MonoBehaviour
{
    private WebSocket ws;

    void Start()
    {
        // ������ Ŭ���̾�Ʈ ���� �� ���� �õ�
        ws = new WebSocket("wss://port-0-weaponregradewebsocket-12fhqa2bln15fvi3.sel5.cloudtype.app"); // ������ URL �� ��Ʈ ��ȣ�� ����

        // ���� �� �̺�Ʈ ó��
        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("������ ����Ǿ����ϴ�.");
            ws.Send("�ȳ��ϼ���, ����!");
        };

        // �޽��� ���� �� �̺�Ʈ ó��
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("�����κ��� �޽��� ����: " + e.Data);
        };

        // ���� ���� �� �̺�Ʈ ó��
        ws.OnClose += (sender, e) =>
        {
            Debug.Log("�������� ������ ����Ǿ����ϴ�.");
        };

        // ���� �õ�
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
