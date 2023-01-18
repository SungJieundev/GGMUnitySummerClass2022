var a = 5; // 다시 선언할 수 있는 변수
const b = 7; 
let c = 3; 


var Jieun = {
    name : "성지은",
    age : 17
}

const ws = require('ws');

const wss = new ws.Server({port : 8080});

wss.on('listening', () => {});

wss.on('connection', client => {
    client.send("반갑습니다");
});

client.on('message', msg => {
    client.send(`네가 나한테 보낸 것은 ${msg.toString()}이야.`)
});