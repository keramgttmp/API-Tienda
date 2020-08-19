function getAllRequest() {
  axios
    .get("https://localhost:44318/api/categoria")
    .then(function (response) {
      console.log(response);
      console.log(response.data);
      var Tabla = document.querySelector("#Tabla");
      Tabla.innerHTML = "";
      for (let vItem of response.data) {
        console.log(vItem.descripcion);
        var vFechaActualizacion = vItem.fechaActualizacion;
        if (vFechaActualizacion == null) {
          vFechaActualizacion = "";
        }

        console.log(
            "<button class=" +
            String.fromCharCode(34) +
            "btn btn-dark" +
            String.fromCharCode(34) +
            " onclick=" +
            String.fromCharCode(34) +
            "VerArticulosPorCategoria(" +
            vItem.id +
            ")" +
            String.fromCharCode(34) +
            ">Modificar</button>" 
        );
        Tabla.innerHTML +=
          "<tr>" +
          "<th>" +
          vItem.id +
          "</th>" +
          "<th>" +
          vItem.descripcion +
          "</th>" +
          "<th>" +
          vItem.fechaCreacion +
          "</th>" +
          "<th>" +
          vFechaActualizacion +
          "</th>" +
          "<th>" +
          "<button class=" +
          String.fromCharCode(34) +
          "btn btn-dark" +
          String.fromCharCode(34) +
          " onclick=" +
          String.fromCharCode(34) +
          "VerArticulosPorCategoria(" +
          vItem.id +
          ")" +
          String.fromCharCode(34) +
          ">Modificar</button>" +
          "</tr>";
      }
    })
    .catch(function (error) {
      console.log(error);
    })
    .then(function () {});
}

function VerArticulosPorCategoria(pCategoriaId) {
  axios
    .get(
      "https://localhost:44318/api/Producto/BuscarPorCategoria?categoriaId=" +
        pCategoriaId
    )
    .then(function (response) {
      console.log(response);
      console.log(response.data);
      /*var Tabla = document.querySelector("#Tabla");
        Tabla.innerHTML = "";
        for (let vItem of response.data) {
          console.log(vItem.descripcion);
          var vFechaActualizacion = vItem.fechaActualizacion;
          if (vFechaActualizacion == null) {
            vFechaActualizacion = "";
          }
  
          console.log("<button onclick="+String.fromCharCode(34)+"VerArticulosPorCategoria("+vItem.id+")"+String.fromCharCode(34)+">Modificar</button>");
          Tabla.innerHTML +=
            "<tr>" +
            "<th>" +
            vItem.id +
            "</th>" +
            "<th>" +
            vItem.descripcion +
            "</th>" +
            "<th>" +
            vItem.fechaCreacion +
            "</th>" +
            "<th>" +
            vFechaActualizacion +
            "</th>" +
            "<th>" +
            "<button onclick="+String.fromCharCode(34)+"deleteRequest("+vItem.id+")"+String.fromCharCode(34)+">Modificar</button>"+
            "</tr>";
        } del For*/
    })
    .catch(function (error) {
      console.log(error);
    })
    .then(function () {});
}

function getFilteredRequest() {
  axios
    .get("http://localhost:8080/item", {
      params: {
        filter: "myFilter",
      },
    })
    .then(function (response) {
      console.log(response);
    })
    .catch(function (error) {
      console.log(error);
    })
    .then(function () {});
}

function getByIdRequest() {
  id = 10;
  axios
    .get("http://localhost:8080/item/" + id)
    .then(function (response) {
      console.log(response);
    })
    .catch(function (error) {
      console.log(error);
    })
    .then(function () {});
}

function postRequest() {
  axios
    .post("http://localhost:8080/item", {
      data: "NewItem",
    })
    .then(function (response) {
      console.log(response);
    })
    .catch(function (error) {
      console.log(error);
    })
    .then(function () {});
}

function putRequest() {
  id = 10;
  axios
    .put("http://localhost:8080/item/" + id, {
      data: "NewItem",
    })
    .then(function (response) {
      console.log(response);
    })
    .catch(function (error) {
      console.log(error);
    })
    .then(function () {});
}

function patchRequest() {
  id = 10;
  axios
    .patch("http://localhost:8080/item/" + id, {
      data: "NewItem",
    })
    .then(function (response) {
      console.log(response);
    })
    .catch(function (error) {
      console.log(error);
    })
    .then(function () {});
}

function deleteRequest() {
  id = 10;
  axios
    .delete("http://localhost:8080/item/" + id)
    .then(function (response) {
      console.log(response);
    })
    .catch(function (error) {
      console.log(error);
    })
    .then(function () {});
}
