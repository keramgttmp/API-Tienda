function fVerCategoria() {
  axios
    .get("https://localhost:44318/api/categoria")
    .then(function (response) {
      console.log(response);
      console.log(response.data);
      var vFiltro = document.querySelector("#filtro");
      $(vFiltro).show();
      /*Formatea el encabezado de la tabla*/
      var vTitulo = document.querySelector("#Titulo");
      vTitulo.textContent = "Listado de Categorías disponibles en la Tienda.";
      var vTablaEncabezado = document.querySelector("#TablaEncabezado");
      vTablaEncabezado.innerHTML =
        "<tr><th>Id</th><th>Descripción</th><th>Fec.Creación</th><th>Fec.Actualiz.</th><th>Acción</th></tr>";

      /*Llena el detalle de la tabla*/
      var vTabla = document.querySelector("#TablaDetalle");
      vTabla.innerHTML = "";
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
        vTabla.innerHTML +=
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
      var vFiltro = document.querySelector("#filtro");
      $(vFiltro).hide();
      /*Formatea el encabezado de la tabla*/
      var vTitulo = document.querySelector("#Titulo");
      vTitulo.textContent = "Listado de Productos disponibles en la Tienda.";
      var vTablaEncabezado = document.querySelector("#TablaEncabezado");
      vTablaEncabezado.innerHTML =
        "<tr><th>Id</th><th>Descripción</th><th>Fec.Creación</th><th>Fec.Actualiz.</th><th>Acción</th></tr>";

      /*Llena el detalle de la tabla*/
      var vTabla = document.querySelector("#TablaDetalle");
      vTabla.innerHTML = "";
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
        vTabla.innerHTML +=
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
          "fVerProductoPorId(" +
          vItem.id +
          ")" +
          String.fromCharCode(34) +
          ">Ver Detalle</button>" +
          "</tr>";
      }
    })
    .catch(function (error) {
      console.log(error);
    })
    .then(function () {});
}

function fObtenerDescripcionCategoria(pCategoriaId, pTitulo) {
  axios
    .get("https://localhost:44318/api/Categoria/" + pCategoriaId)
    .then(function (response) {
      console.log(response);
      console.log(response.data.descripcion);
      var Descripcion = response.data.descripcion;
      pTitulo.textContent += Descripcion;
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

      var vControl = 0;
      for (let vItem of response.data) {
        if (vControl == 0) {
          var vFiltro = document.querySelector("#filtro");
          $(vFiltro).hide();
          /*Formatea el encabezado de la tabla*/
          var vTitulo = document.querySelector("#Titulo");
          var vDescripcionCategoria = fObtenerDescripcionCategoria(
            pCategoriaId,
            vTitulo
          );
          /*vTitulo.textContent =
            "Listado de Productos disponibles en la Tienda. " +
            DescripcionCategoria;*/
          var vTablaEncabezado = document.querySelector("#TablaEncabezado");
          vTablaEncabezado.innerHTML =
            "<tr><th>Id</th><th>Descripción</th><th>Fec.Creación</th><th>Fec.Actualiz.</th><th>Acción</th></tr>";

          /*Llena el detalle de la tabla*/
          var vTabla = document.querySelector("#TablaDetalle");
          vTabla.innerHTML = "";
          vControl = 1;
        }
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
        vTabla.innerHTML +=
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
          "fVerProductoPorId(" +
          vItem.id +
          ")" +
          String.fromCharCode(34) +
          ">Ver Detalle</button>" +
          "</tr>";
      }
      if (vControl == 0) {
        alert("Esta categoría no tiene productos asociados");
      }
    })
    .catch(function (error) {
      console.log(error);
    })
    .then(function () {});
}

function fVerProductoPorId(pProductoId) {
  axios
    .get("https://localhost:44318/api/Producto/" + pProductoId)
    .then(function (response) {
      var vComillas = String.fromCharCode(34);
      var vModal = document.getElementById("modal");
      var vIconBar = document.getElementById("iconbar");
      modal.style.display = "block";
      var vTituloModal = document.getElementById("titulomodal");
      vTituloModal.innerText = response.data.descripcion;
      var vContenidoModal = document.getElementById("modalcontenido");

      vContenidoModal.innerHTML =
        "<ul class=" +
        vComillas +
        "price" +
        vComillas +
        ">" +
        "<li class=" +
        vComillas +
        "grey" +
        vComillas +
        ">$" +
        response.data.precio +
        "</li>" +
        "<li>Cant.Dispon. " +
        response.data.cantidad +
        "</li>" +

        "<li>Quiero " + "<input id="+
        vComillas +"cantidad"+
        vComillas +" type="+
        vComillas +"number"+
        vComillas +" min="+
        vComillas +"1"+
        vComillas +" max="+
        vComillas +response.data.cantidad+
        vComillas +" required></input>"+
        "</li>" +

        "</ul>";

      /*arma el barra de íconos del footer en la modal*/
      vIconBar.innerHTML = "";
      vIconBar.innerHTML =
        "<a href=" +
        vComillas +
        "#" +
        vComillas +
        " onclick="+
        vComillas +"fVerCliente()"+
        vComillas +" ><i class=" +
        vComillas +
        "fa fa-cart-plus" +
        vComillas +
        " aria-hidden=" +
        vComillas +
        "true" +
        vComillas +
        "></i> Comprar</i></a>" +
        "<a href=" +
        vComillas +
        "#" +
        vComillas +
        " onclick="+
        vComillas +"fVerCliente()"+
        vComillas +" ><i class=" +
        vComillas +
        "fa fa-commenting" +
        vComillas +
        " aria-hidden=" +
        vComillas +
        "true" +
        vComillas +
        "></i> Comentar </i></a>";
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
      var vFiltro = document.querySelector("#filtro");
      $(vFiltro).hide();
      /*Formatea el encabezado de la tabla*/
      var vTitulo = document.querySelector("#Titulo");
      vTitulo.textContent = "Listado de Clientes de la Tienda.";
      var vTablaEncabezado = document.querySelector("#TablaEncabezado");
      vTablaEncabezado.innerHTML =
        "<tr><th>Id</th><th>Nombre</th><th>Fec.Creación</th><th>Fec.Actualiz.</th><th>Acción</th></tr>";

      /*Llena el detalle de la tabla*/
      var vTabla = document.querySelector("#TablaDetalle");
      vTabla.innerHTML = "";
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
        vTabla.innerHTML +=
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
