<%@ taglib prefix="a" uri="http://jmaki/v1.0/jsp" %>
<%@ page contentType="text/html; charset=iso-8859-1" language="java" import="java.sql.*" errorPage="" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<title>Documento sin t&iacute;tulo</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<script type="text/javascript" language="javascript">

function MostrarOcultarProcesando(esMostrar) 
{
	if(esMostrar)
	{
		document.frmExpandirColapsar.imgEstadoProceso.style.visibility="visible";
	}
	else
	{
		document.frmExpandirColapsar.imgEstadoProceso.style.visibility="hidden";
	}
}

 function colapsar() 
{
  	var symbol="";
	symbol=top.getElementByIds();
	
	document.frmExpandirColapsar.imgExpandirColapsar.src="Imagenes/"+symbol;
}

function CerrarPagina() 
{
	window.close();
}

function IrAreaDePagina(area) 
{
	var hrefPagina=top.mainFrame.document.location.href.split('#')[0];
		
	if(area=='Acciones')
	{
		top.mainFrame.document.location.href=hrefPagina+'#Acciones';		
	}
	else if (area=='Campos')
	{
		top.mainFrame.document.location.href=hrefPagina+'#Campos';
	}
	else if (area=='Busquedas')
	{
		top.mainFrame.document.location.href=hrefPagina+'#Busquedas';
	}
	
} 
   
</script>
</head>
<body>
<table width="100%"  border="0">
  <tr>
    <td bgcolor="#000099">&nbsp;
    
    </td>
  </tr>
  <tr>
    <td >&nbsp;</td>
  </tr>
  <tr>
    <td bgcolor="#000099">&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td bgcolor="#000099">
</td>
  </tr>
</table>

<form name="frmExpandirColapsar">

<table width="100%"  border="0">
<tr align="left" style="width: 505px">
<td align="left">
<img id="imgExpandirColapsar" align="rigth" style="visibility:visible" src="Imagenes/collapse.gif" width="20" height="20"  onclick="colapsar()"/>

</td>

<td align="left" style="width: 258px">
<img align="left" id="imgEstadoProceso" style="visibility:hidden; width: 55px; height: 54px" src="Imagenes/wait2.gif" width="32" height="32" />
</td>

<td align="rigth" style="width: 98px">
	<img id="imgAreaBusquedas" align="rigth" style="visibility:visible" src="Imagenes/busqueda.gif" width="20" height="20"  onclick="IrAreaDePagina('Busquedas')"/>	
	<img id="imgAreaControles" align="rigth" style="visibility:visible" src="Imagenes/controles.gif" width="20" height="20"  onclick="IrAreaDePagina('Campos')"/>
	<img id="imgAreaAcciones" align="rigth" style="visibility:visible" src="Imagenes/acciones.gif" width="20" height="20"  onclick="IrAreaDePagina('Acciones')"/>	
</td>

</tr>

</table>

</form>
</body>
</html>