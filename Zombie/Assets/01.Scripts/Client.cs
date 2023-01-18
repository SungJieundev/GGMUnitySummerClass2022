using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;

[Serializable]
public class Packet{
    public string locate = "";
    public string type = "";
    public string value = "";

    public Packet(string locate, string type, string value)
    {
        this.locate = locate;
        this.type = type;
        this.value = value;
    }
}

[Serializable]
public class VectorPacket{
    public float x;
    public float y;
    public float z;

    public VectorPacket(Vector3 pos){
        this.x = pos.x;
        this.y = pos.y;
        this.z = pos.z;
    }
}

public class Client : MonoBehaviour
{
    public static Client Instance = null;

    private Queue<Action> actions = new Queue<Action>(); // 업데이트
    private Queue<Action> physicsActions = new Queue<Action>(); 

    private object locker = new object();

    [SerializeField] private string url = "ws://127.0.0.1:8080";
    private WebSocket ws = null;
    [SerializeField] private GameObject otherPlayer = null;

    private OtherMovement om = null;

    private void Awake() {
        ws = new WebSocket(url);
        ws.ConnectAsync();

        if(Instance = null){
            Instance = this;
        }

        ws.OnMessage += MessageManager; 
    }

    private void FixedUpdate() {
        if(physicsActions.Count > 0) physicsActions.Dequeue()?. Invoke();
    }
    private void Update() {
        if(actions.Count > 0) actions.Dequeue()?.Invoke();
    }

    private void MessageManager(object sender, MessageEventArgs e){

        lock(locker){
            Packet p = JsonUtility.FromJson<Packet>(e.Data);
            switch(p.locate)
            {
                case "room":
                    actions.Enqueue(() => RoomData(p));
                    break;
                case "game":
                    actions.Enqueue(() => GameData(p));
                    break;
            }
        }
    }

    private void GameData(Packet p){
        switch(p.type)
        {
            case "move":
                physicsActions.Enqueue(() => {
                    VectorPacket mv = JsonConvert.DeserializeObject<VectorPacket>(p.value);
                    om.SetPosition(new Vector3(mv.x, mv.y, mv.z));
                });
                break;
            case "rotate":
                physicsActions.Enqueue(()=> {
                    VectorPacket rv = JsonConvert.DeserializeObject<VectorPacket>(p.value);
                    om.SetRotate(rv.y);
                });
                break;
        }
    }

    private void RoomData(Packet p){
        switch(p.type){
            // case "join":
            //     GameObject.Find("HUD Canvas/WaitPanel").SetActive(false);
            //     Time.timeScale = 1;
            //     GameObject obj = Instantiate(otherPlayer, Vector3.zero, Quaternion.identity);
            //     om = obj.GetComponent<OtherMovement>();
            //     break;
        }
    }

    public void SendMsgToServer(string locate, string type, string value){
        ws.Send(JsonUtility.ToJson(new Packet(locate, type, value)));
    }

    public void SendMsgToServer(string locate, string type, object value){
        string JSON = JsonConvert.SerializeObject(value);
        ws.Send(JsonUtility.ToJson(new Packet(locate, type, JSON)));
    }
}
