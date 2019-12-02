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
    socket.on("enter", obj => {
      let div = document.createElement("div");
      div.innerHTML = `${obj.date} ${obj.name}: ${obj.msg}`;
      postsBox.append(div);
    });

    socket.on("posts", data => {
      if (!data) return;
      data.forEach(el => {
        let div = document.createElement("div");
        div.innerHTML = `${el.date} ${el.name}: ${el.msg}`;
        postsBox.append(div);
      });
    });

    socket.on("newPost", obj => {
      let div = document.createElement("div");
      div.innerHTML = `${obj.date} ${obj.name}: ${obj.msg}`;
      postsBox.append(div);
    });

    socket.on("leave", obj => {
      let div = document.createElement("div");
      div.innerHTML = `${obj.date} ${obj.name}: ${obj.msg}`;
      postsBox.append(div);
    });
  });
})();
