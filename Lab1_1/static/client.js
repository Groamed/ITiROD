(function() {
  let socket = io();
  let postsBox = document.querySelector(".posts");
  let msgInput = document.querySelector(".msg");
  let sendBut = document.querySelector(".trigger");

  sendBut.addEventListener("click", () => {
    socket.emit("message", msgInput.value);
    msgInput.value = "";
  });

  socket.on("connect", () => {
    socket.on("enter", msg => {
      let div = document.createElement("div");
      div.innerHTML = msg;
      postsBox.append(div);
    });

    socket.on("posts", msg => {
      let data = JSON.parse(msg);
      data.forEach(el => {
        let div = document.createElement("div");
        div.innerHTML = el;
        postsBox.append(div);
      });
    });

    socket.on("newPost", msg => {
      let div = document.createElement("div");
      div.innerHTML = msg;
      postsBox.append(div);
    });

    socket.on("leave", msg => {
      let div = document.createElement("div");
      div.innerHTML = msg;
      postsBox.append(div);
    });
  });
})();
