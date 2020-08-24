function fVerCategoria() {
  axios
    .get("https://localhost:44318/api/categoria")
    .then(function (response) {
      console.log(response);
      console.log(response.data);
      /*Formatea el encabezado de la tabla*/
      var Titulo = document.querySelector("#Titulo");
      Titulo.textContent = "Listado de Categorías disponibles en la Tienda.";
      var TablaEncabezado = document.querySelector("#TablaEncabezado");
      TablaEncabezado.innerHTML =
        "<tr><th>Id</th><th>Descripción</th><th>Fec.Creación</th><th>Fec.Actualiz.</th><th>Acción</th></tr>";

      /*Llena el detalle de la tabla*/
      var Tabla = document.querySelector("#TablaDetalle");
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
          "fVerProductosPorCategoria(" +
          vItem.id +
          ")" +
          String.fromCharCode(34) +
          ">Ver Productos</button>" +
          "</tr>";
      }
    })
    .catch(function (error) {
      console.log(error);
    })
    .then(function () {});
}

function fVerProducto() {
  axios
    .get("https://localhost:44318/api/Producto")
    .then(function (response) {
      console.log(response);
      console.log(response.data);
      /*Formatea el encabezado de la tabla*/
      var Titulo = document.querySelector("#Titulo");
      Titulo.textContent = "Listado de Productos disponibles en la Tienda.";
      var TablaEncabezado = document.querySelector("#TablaEncabezado");
      TablaEncabezado.innerHTML =
        "<tr><th>Id</th><th>Descripción</th><th>Fec.Creación</th><th>Fec.Actualiz.</th><th>Acción</th></tr>";

      /*Llena el detalle de la tabla*/
      var Tabla = document.querySelector("#TablaDetalle");
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
          "fVerArticulosPorCategoria(" +
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

function fObtenerDescripcionCategoria(pCategoriaId) {
  axios
    .get("https://localhost:44318/api/Categoria/"+pCategoriaId)
    .then(function (response) {
      console.log(response);
      console.log(response.data.descripcion);
      var Descripcion = response.data.descripcion;
      return Descripcion;
    })
    .catch(function (error) {
      console.log(error);
    })
    .then(function () {});
}

function fVerProductosPorCategoria(pCategoriaId) {
  axios
    .get(
      "https://localhost:44318/api/Producto/BuscarPorCategoria?categoriaId=" +
        pCategoriaId
    )
    .then(function (response) {
      console.log(response);
      console.log(response.data);
      /*Formatea el encabezado de la tabla*/
      var DescripcionCategoria = fObtenerDescripcionCategoria(pCategoriaId);
      var Titulo = document.querySelector("#Titulo");
      Titulo.textContent = "Listado de Productos disponibles en la Tienda. " + DescripcionCategoria;
      var TablaEncabezado = document.querySelector("#TablaEncabezado");
      TablaEncabezado.innerHTML =
        "<tr><th>Id</th><th>Descripción</th><th>Fec.Creación</th><th>Fec.Actualiz.</th><th>Acción</th></tr>";

      /*Llena el detalle de la tabla*/
      var Tabla = document.querySelector("#TablaDetalle");
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

function fVerCliente() {
  axios
    .get("https://localhost:44318/api/Cliente")
    .then(function (response) {
      console.log(response);
      console.log(response.data);
      /*Formatea el encabezado de la tabla*/
      var Titulo = document.querySelector("#Titulo");
      Titulo.textContent = "Listado de Clientes de la Tienda.";
      var TablaEncabezado = document.querySelector("#TablaEncabezado");
      TablaEncabezado.innerHTML =
        "<tr><th>Id</th><th>Nombre</th><th>Fec.Creación</th><th>Fec.Actualiz.</th><th>Acción</th></tr>";

      /*Llena el detalle de la tabla*/
      var Tabla = document.querySelector("#TablaDetalle");
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
          vItem.nombre +
          " " +
          vItem.primerApellido +
          " " +
          vItem.segundoApellido +
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
