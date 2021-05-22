
const WebSocket = require('ws');
process.env['NODE_TLS_REJECT_UNAUTHORIZED'] = 0

let socketUrl = 'wss://localhost:6868'
socket = new WebSocket(socketUrl);


let clientID = "LJGRcgyRuuMF6SddP3jxuoMdtxctj1zjkcGhNXQi";
let clientSecret = "hlkwqCw9Fe0Li4peGQHb5jgvOObJwJnL9W8IfkSjrvJ1ndh3UucSS3lTGCRGmEvDFEBZ1b6ZB92bFE6dY9q0j77Tg7CwYIMIAMnE26CGwwqJvFoBSfA8NiAp1TZOi3fi";

let cortexToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhcHBJZCI6ImNvbS5hdHVsYWNoYXJ5YTE3Lm1pbmR6b25lIiwiYXBwVmVyc2lvbiI6IjEuMCIsImV4cCI6MTYyMTg0Mjk1NSwibmJmIjoxNjIxNTgzNzU1LCJ1c2VySWQiOiI5MjBhZTA2ZS1lNDJhLTQ0ZWEtYjVhMS1kMGI1YThkNmIwYjYiLCJ1c2VybmFtZSI6ImF0dWxhY2hhcnlhMTciLCJ2ZXJzaW9uIjoiMi4wIn0=.Yg9Z6kuVLa7PPhU5sTwPjinWzNVjZE0iWnIsTCHlU8g="

let device = "EPOCPLUS-3B9ACA8A"

let sessionID = "59b746a6-1339-4944-9199-257a7b7fd050"

initialQuery = {"id":1,"jsonrpc":"2.0","method":"getCortexInfo"}

getUserLogin = {
  "id": 1,
  "jsonrpc": "2.0",
  "method": "getUserLogin"
}

requestAccess = {
  "id": 1,
  "jsonrpc": "2.0",
  "method": "requestAccess",
  "params": {
      "clientId": clientID,
      "clientSecret": clientSecret
  }
}

authorize = {
  "id": 1,
  "jsonrpc": "2.0",
  "method": "authorize",
  "params": {
      "clientId": clientID,
      "clientSecret": clientSecret
  }
}

userInfo = {
  "id": 1,
  "jsonrpc": "2.0",
  "method": "getUserInformation",
  "params": {
      "cortexToken": cortexToken
  }
}

licenseInfo = {
  "id": 1,
  "jsonrpc": "2.0",
  "method": "getLicenseInfo",
  "params": {
      "cortexToken": cortexToken
  }
}

headset = {
  "id": 1,
  "jsonrpc": "2.0",
  "method": "queryHeadsets"
}

control = {
  "id": 1,
  "jsonrpc": "2.0",
  "method": "controlDevice",
  "params": {
      "command": "connect",
      "headset": device
  }
}

startSession = {
  "id": 1,
  "jsonrpc": "2.0",
  "method": "createSession",
  "params": {
      "cortexToken": cortexToken,
      "headset": device,
      "status": "open"
  }
}

querySessions = {
  "id": 1,
  "jsonrpc": "2.0",
  "method": "querySessions",
  "params": {
      "cortexToken": cortexToken
  }
}

subscribe = {
  "id": 1,
  "jsonrpc": "2.0",
  "method": "subscribe",
  "params": {
      "cortexToken": cortexToken,
      "session": sessionID,
      "streams": ["met","mot"]
  }
}

function function2() {
  socket.send(JSON.stringify(startSession));
}

socket.onopen = function (event) {
  console.log("\nSending Data\n\n");
  console.log("Reply from server:\n");
  //socket.send(JSON.stringify(userInfo));
  //socket.send(JSON.stringify(control));
  socket.send(JSON.stringify(querySessions));

  //setTimeout(function1, 5000);

  //setTimeout(function2, 120000);

  
};

function function1() {
  socket.send(JSON.stringify(headset));
  
}


socket.onmessage = function (event) {
  console.log(event.data);
}
