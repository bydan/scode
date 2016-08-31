
<%@ taglib prefix="a" uri="http://jmaki/v1.0/jsp" %>
<%@ page contentType="text/html; charset=iso-8859-1" language="java" import="java.sql.*" errorPage="" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<script type="text/javascript" language="javascript" src="JavaScript/ajax.js"><//script>
<script type="text/javascript" language="javascript" src="JavaScript/FuncionesGeneralesAjax.js"></script>
<script type="text/javascript" language="javascript" src="JavaScript/FuncionesGenerales.js"></script>


<title>Aplicacion</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">

<style type="text/css">
body {
	background-image: url(Imagenes/background4.jpg);
	background-repeat: repeat-Y
}
</style>
<%
	String strPerfilArbol="";
	
	strPerfilArbol+="\r\n{";
    strPerfilArbol+="\r\n\troot : {";
    strPerfilArbol+="\r\n\tlabel : 'SISTEMA OPEN SOURCE',";
    strPerfilArbol+="\r\n\texpanded : true,";
    strPerfilArbol+="\r\n\tchildren : [";
    strPerfilArbol+="\r\n\t{ label : 'CATALOGOS SIMPLES',";
    strPerfilArbol+="\r\n\t\texpanded : true,";
    strPerfilArbol+="\r\n\t\tchildren : [";
 	
	
    strPerfilArbol+="\r\n\t\t]";
    strPerfilArbol+="\r\n\t},";
	strPerfilArbol+="\r\n\t{ label : 'CATALOGOS COMPUESTOS',";
    strPerfilArbol+="\r\n\t\texpanded : true,";
    strPerfilArbol+="\r\n\t\tchildren : [";
 	strPerfilArbol+="\r\n\t\t";
    strPerfilArbol+="\r\n\t\t]";
    strPerfilArbol+="\r\n\t\t}";
    strPerfilArbol+="\r\n\t]";
    strPerfilArbol+="\r\n\t}";
    strPerfilArbol+="\r\n\t}";
		
	request.getSession().setAttribute("valueTreeAjax",strPerfilArbol);
	
%>

<%

	String strPerfilJavaScriptArbol="";
	
	strPerfilJavaScriptArbol+="\r\nif(args.message.targetId==''||args.message.targetId==null)";
	strPerfilJavaScriptArbol+="\r\n{";
	strPerfilJavaScriptArbol+="\r\n\tpagina='FinContenido.jsp';";
	strPerfilJavaScriptArbol+="\r\n}";

	
	
	request.getSession().setAttribute("valueJavaScriptTreeAjax",strPerfilJavaScriptArbol);
	
%>	

</head>
<body >
<input name="logo" type="image" src="Imagenes/Logos/Tame1.jpg" width="200" height="100">

<br>
 


 


</body>
</html>

