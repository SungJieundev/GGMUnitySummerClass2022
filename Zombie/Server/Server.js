const crypto = require('crypto'); // 보안이나 암호화, 랜덤 등을 할 수 있는
const ws = require('ws');

const wss = new ws.Server({port:8080});
// 추가 +++++++++++++++++++++++++++++++++
var gameList = {};

wss.on('listening', () => {
    console.log(`Server open on port ${wss.options.port}`);
});

wss.on('connection', client => {
    client.UUID = crypto.randomUUID();

    // if(wss.clients.size >= 2){
    //     Broadcast(wss.clients, JSON.stringify({ locate : "room", type : "join", value : ""}));
    // }

    client.on('message', msg => {
        const data = stringToObject(msg.toString());
        if(data == false) return;

        switch(data.locate)
        {
            case 'room':
                RoomData(data, client);
                break;
            case 'game':
                GameData(data, client);
                break;
        }
    });
    client.on('close', () => {});
});

function GameData(data, client)
{
    switch(data.type)
    {
        case 'move':
        case 'rotate':
            Broadcast(wss.clients, JSON.stringify(data), client.UUID);
            break;
        case 'fire':
            break;
        case 'getItem':
            break;
    }
}

function RoomData(){
    
}

function stringToObject(stringData){
    try
    {
        return JSON.parse(stringData);
    }
    catch(error)
    {   
        console.log("data is not JSON data");
        return false;
    }
}


function Broadcast(array, data, UUID) {
    if(UUID == undefined){
        array.forEach(client => {
            client.send(data);
        });
    }
    else{
        array.forEach(client => {
            if(client.UUID != UUID){
                client.send(data);
            }
        })
    }
}