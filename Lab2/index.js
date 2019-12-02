const express = require("express");
const app = express();
const port = 3000;
const server = require("http").createServer(app);
const io = require("socket.io").listen(server);
const fs = require("fs");
const MongoClient = require("mongodb").MongoClient;

let dbClient;

const mgc = new MongoClient("mongodb://localhost:27017/", {
  useNewUrlParser: true
});

mgc.connect((err, client) => {
  if (err) console.log(err);
  dbClient = client;

  server.listen(port, () => {
    console.log("Server is working");
  });
});

app.use("/static", express.static("static"));
app.get("/", function(req, res) {
  res.sendFile(__dirname + "/index.html");
});

io.on("connection", socket => {
  let ID = socket.id.toString().substr(0, 5);

  dbClient
    .db("userdb")
    .collection("posts")
    .find()
    .toArray()
    .then(res => {
      socket.emit("posts", res);
      socket.broadcast.emit("enter", format("Зашел в чат", ID, new Date()));
    });
  socket.on("message", msg => format(msg, ID, new Date()));
  socket.on("disconnect", () => format("Вышел из чата", ID, new Date()));
});

function format(msg, name, date) {
  let month = [
    "Jan",
    "Feb",
    "Mar",
    "Apr",
    "May",
    "Jun",
    "Jul",
    "Aug",
    "Sep",
    "Oct",
    "Nov",
    "Dec"
  ];
  let data = dbClient.db("userdb").collection("posts");
  let post = {
    name,
    msg,
    date: `[${date.getDate()}.${
      month[date.getMonth()]
    }.${date.getFullYear()} ${date.getHours()}:${date.getMinutes()}]`
  };

  data.insertOne(post, readAndSend);
}

function readAndSend() {
  dbClient
    .db("userdb")
    .collection("posts")
    .find()
    .sort({ $natural: -1 })
    .toArray()
    .then(res => io.sockets.emit("newPost", res[0]));
}

process.on("SIGINT", () => {
  dbClient.close();
  process.exit();
});
