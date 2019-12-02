const express = require("express");
const app = express();
const port = 3000;
const server = require("http").createServer(app);
const io = require("socket.io").listen(server);
const fs = require("fs");

app.use("/static", express.static("static"));
app.get("/", function(req, res) {
  res.sendFile(__dirname + "/index.html");
});

server.listen(port, () => {
  console.log("Server is working");
});

io.on("connection", socket => {
  let ID = socket.id.toString().substr(0, 5);
  socket.emit("posts", fs.readFileSync("static/messages.json", "utf8") || "[]");
  socket.broadcast.emit("enter", format("Зашел в чат", ID, new Date()));
  socket.on("message", msg =>
    io.sockets.emit("newPost", format(msg, ID, new Date()))
  );
  socket.on("disconnect", () =>
    io.sockets.emit("leave", format("Вышел из чата", ID, new Date()))
  );
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
  let data = fs.readFileSync("static/messages.json", "utf8") || "[]";
  data = JSON.parse(data);
  let post = `[${date.getDate()}.${
    month[date.getMonth()]
  }.${date.getFullYear()} ${date.getHours()}:${date.getMinutes()}] ${name}: ${msg}`;

  data.push(post);
  fs.writeFileSync("static/messages.json", JSON.stringify(data));

  return post;
}
