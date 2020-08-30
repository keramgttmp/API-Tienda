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
      var vTextoGeneral = document.querySelector("#textogeneral");
      vTextoGeneral.textContent = "Desde esta opción podrá visualizar las categorías habilitadas y navegar a los productos contenidos en la misma."
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
      var vTextoGeneral = document.querySelector("#textogeneral");
      vTextoGeneral.textContent = "Desde esta opción podrá ver el listado de Productos y comprar el item que sea de su interés."

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

function fVerOrdenes() {
  axios
    .get("https://localhost:44318/api/Orden")
    .then(function (response) {
      console.log(response);
      console.log(response.data);
      var vFiltro = document.querySelector("#filtro");
      $(vFiltro).hide();
      /*Formatea el encabezado de la tabla*/
      var vTitulo = document.querySelector("#Titulo");
      vTitulo.textContent = "Listado de Órdenes ingresadas en la Tienda.";
      var vTextoGeneral = document.querySelector("#textogeneral");
      vTextoGeneral.textContent = "Puede consultar el listado general de las órdenes que han sido emitidas desde nuestra Tienda en el proceso de compra."
      var vTablaEncabezado = document.querySelector("#TablaEncabezado");
      vTablaEncabezado.innerHTML =
        "<tr><th>Id</th><th>ClienteId</th><th>Fec.Orden</th><th>Monto</th><th>Acción</th></tr>";

      /*Llena el detalle de la tabla*/
      var vTabla = document.querySelector("#TablaDetalle");
      vTabla.innerHTML = "";
      for (let vItem of response.data) {
        var vFechaActualizacion = vItem.fechaActualizacion;
        if (vFechaActualizacion == null) {
          vFechaActualizacion = "";
        }

        vTabla.innerHTML +=
          "<tr>" +
          "<th>" +
          vItem.id +
          "</th>" +
          "<th>" +
          vItem.clienteId +
          "-CLIENTE" +
          "</th>" +
          "<th>" +
          vItem.fecha +
          "</th>" +
          "<th>" +
          vItem.montoTotal +
          "</th>" +
          "<th>" +
          "<button class=" +
          String.fromCharCode(34) +
          "btn btn-dark" +
          String.fromCharCode(34) +
          " onclick=" +
          String.fromCharCode(34) +
          "fVerDetalleOrden(" +
          vItem.id +
          ")" +
          String.fromCharCode(34) +
          ">Ver Detalle</button>" +
          "</tr>";

        //Busca el cliente para mostrarlo
        axios
          .get(
            "https://localhost:44318/api/Cliente/" + vItem.clienteId
          )
          .then(function (response) {
            var vNombreCliente = response.data.nombre + " "+ response.data.primerApellido +" "+response.data.segundoApellido;
            vTabla.innerHTML = vTabla.innerHTML.replace(
              "CLIENTE",
              vNombreCliente
            );
          })
          .catch(function (error) {
            console.log(error);
          })
          .then(function () {});
      }
    })
    .catch(function (error) {
      console.log(error);
    })
    .then(function () {});
}

//Muestra los productos filtrado por el id, dentro de una modal
function fVerDetalleOrden(pOrdenId) {
  axios
    .get("https://localhost:44318/api/DetalleOrden/" + pOrdenId)
    .then(function (response) {
      var vComillas = String.fromCharCode(34);
      var vModal = document.getElementById("modal");
      var vIconBar = document.getElementById("iconbar");
      modal.style.display = "block";
      var vTituloModal = document.getElementById("titulomodal");
      vTituloModal.innerText = "Detalle de orden #" + response.data.ordenId;
      var vContenidoModal = document.getElementById("modalcontenido");
      var vDescripcionProducto = "";
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
        ">Artículo " +
        response.data.productoId +
        "-Descripcion" +
        "</li>" +
        "<li>Cant.Solicitada " +
        response.data.cantidadDetalle +
        "</li>" +
        "<li>Precio detalle $" +
        response.data.precioDetalle +
        "</li>" +
        "</ul>";

      /*limpia barra de íconos del footer en la modal*/
      vIconBar.innerHTML = "";
      //Busca el producto para mostrarlo
      axios
        .get("https://localhost:44318/api/Producto/" + response.data.productoId)
        .then(function (response) {
          vDescripcionProducto = response.data.descripcion;
          vContenidoModal.innerHTML = vContenidoModal.innerHTML.replace(
            "Descripcion",
            vDescripcionProducto
          );
        })
        .catch(function (error) {
          console.log(error);
        })
        .then(function () {});
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

//Muestra los productos filtrado por el id, dentro de una modal
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
        "<li>Quiero " +
        "<input id=" +
        vComillas +
        "cantidad" +
        vComillas +
        " type=" +
        vComillas +
        "number" +
        vComillas +
        " min=" +
        vComillas +
        "1" +
        vComillas +
        " max=" +
        vComillas +
        response.data.cantidad +
        vComillas +
        " required></input>" +
        "</li>" +
        "</ul>";

      /*arma el barra de íconos del footer en la modal*/
      vIconBar.innerHTML = "";
      vIconBar.innerHTML =
        "<a href=" +
        vComillas +
        "#" +
        vComillas +
        " onclick=" +
        vComillas +
        "fComprarProducto(" +
        pProductoId +
        "," +
        response.data.precio +
        ")" +
        vComillas +
        " ><i class=" +
        vComillas +
        "fa fa-cart-plus" +
        vComillas +
        " aria-hidden=" +
        vComillas +
        "true" +
        vComillas +
        "></i> Comprar</i></a>";
        
        //asigno por defecto el valor de uno.
        $("#cantidad").val(1);
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
      var vTextoGeneral = document.querySelector("#textogeneral");
      vTextoGeneral.textContent = "Desde esta opción podrá consultar los clientes registros y editar sus propiedades."

      var vTablaEncabezado = document.querySelector("#TablaEncabezado");
      vTablaEncabezado.innerHTML =
        "<tr><th>Id</th><th>Nombre</th><th>Fec.Creación</th><th>Fec.Actualiz.</th><th>Acción</th></tr>";

      /*Llena el detalle de la tabla*/
      var vTabla = document.querySelector("#TablaDetalle");
      vTabla.innerHTML = "";
      for (let vItem of response.data) {
        var vFechaActualizacion = vItem.fechaActualizacion;
        if (vFechaActualizacion == null) {
          vFechaActualizacion = "";
        }

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
          "fEditarCliente(" +
          vItem.id +
          ")" +
          String.fromCharCode(34) +
          ">Editar</button>" +
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

function fComprarProducto(pProductoId, pPrecio) {
  var vNumeroOrden = 0;
  var vPrecioTotal = 0;
  var vCantidad = $("#cantidad").val();
  if (Number(vCantidad) >= 1 && !vCantidad == "") {
    var vFecha = new Date().toISOString().substr(0, 19);
    console.log(vFecha);
    vPrecioTotal = pPrecio * vCantidad;
    //Post del encabezado de la orden
    axios
      .post("https://localhost:44318/api/Orden", {
        fecha: "2020-08-29T04:41:28.227Z",
        clienteId: 2,
        montoTotal: vPrecioTotal,
        estado: "A",
        fechaCreacion: vFecha,
      })
      .then(function (response) {
        console.log(response);
        vNumeroOrden = response.data.id;
        console.log("1 ORden creada " + vNumeroOrden);

        // Post del detalle de la orden
        axios
          .post("https://localhost:44318/api/DetalleOrden", {
            ordenId: vNumeroOrden,
            productoId: pProductoId,
            cantidadDetalle: Number(vCantidad),
            precioDetalle: vPrecioTotal,
            fechaCreacion: vFecha,
          })
          .then(function (response) {
            console.log(response);
            console.log("2 Detalle orden creada " + response.data.id);
            alert("Se creó la orden de compra " + vNumeroOrden);
            var vModal = document.getElementById("modal");
            modal.style.display = "none";
          })
          .catch(function (error) {
            console.log(error);
            alert("Hubo un error al crear el detalle de la compra.");
          })
          .then(function () {});
        /**/
      })
      .catch(function (error) {
        console.log(error.message);
        alert("Hubo un error al crear la orden de la compra.");
      })
      .then(function () {});
  } else {
    alert("La cantidad debe ser mayor a 0");
  }
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
