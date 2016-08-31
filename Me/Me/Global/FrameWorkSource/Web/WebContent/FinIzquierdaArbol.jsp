<%@ taglib prefix="a" uri="http://jmaki/v1.0/jsp" %>
<%@ page contentType="text/html; charset=iso-8859-1" language="java" import="java.sql.*" errorPage="" %>
<%@ taglib prefix="a" uri="http://jmaki/v1.0/jsp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<script type="text/javascript" language="javascript" src="JavaScript/ajax.js"></script>
<script type="text/javascript" language="javascript" src="JavaScript/FuncionesGeneralesAjax.js"></script>
<script type="text/javascript" language="javascript" src="JavaScript/FuncionesGenerales.js"></script>


<title>Temas</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">

<style type="text/css">
<!--
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	background-image: url(Imagenes/background4.jpg);
	background-repeat: repeat-y
}
-->
</style>
</head>
<body >
<input name="logo" type="image" src="Imagenes/Logos/Tame1.jpg" width="200" height="100">


   <%
 String userArbol="{'root' : {'title' : '<B>Temas','expanded' : true, 'children' : [{ '' : ''}]}}";
 
 if(request.getSession().getAttribute("valueTreeAjax")!=null)
 userArbol=(String) request.getSession().getAttribute("valueTreeAjax");
%>


<br>
<br>
<br>

<a:widget id="trvTest223" name="dojo.dijit.tree"
        value="<%=userArbol%>"/>

  
  <script type="text/javascript" language="javascript">
  
jmaki.subscribe("/foo/select", manejarSeleccion);
						 
function manejarSeleccion(args) 
{	
	var pagina='FinContenido.jsp';

	if(args.message.targetId=='MantenimientoArea.jsp')
	{
		pagina='MantenimientoArea.jsp';
	}				
	else if(args.message.targetId=='MantenimientoFuncion.jsp')
	{
		pagina='MantenimientoFuncion.jsp';
	}
	else if(args.message.targetId=='MantenimientoParametroAnual.jsp')
	{
		pagina='MantenimientoParametroAnual.jsp';
	}
	else if(args.message.targetId=='MantenimientoCaracteristicaReactivo.jsp')
	{
		pagina='MantenimientoCaracteristicaReactivo.jsp';
	}
	else if(args.message.targetId=='MantenimientoEmpleado.jsp')
	{
		pagina='MantenimientoEmpleado.jsp';
	}
	else if(args.message.targetId=='ProcesarCargarFotoEmpleado.jsp')
	{
		pagina='ProcesarCargarFotoEmpleado.jsp';
	}
	else if(args.message.targetId=='ProcesarAreasAProcesar.jsp')
	{
		pagina='ProcesarAreasAProcesar.jsp';
	}
	else if(args.message.targetId=='EmpleadoRegistrarAsistencia.jsp')
	{
		pagina='EmpleadoRegistrarAsistencia.jsp';
	}
	else if(args.message.targetId=='ProcesarEmpleadoAsistencia.jsp')
	{
		pagina='ProcesarEmpleadoAsistencia.jsp';
	}
	else if(args.message.targetId=='ProcesarAgregarQuitarEmpleadosSorteados.jsp')
	{
		pagina='ProcesarAgregarQuitarEmpleadosSorteados.jsp';
	}
	else if(args.message.targetId=='ProcesarMantenimientoResultadoExamenEmpleado.jsp')
	{
		pagina='ProcesarMantenimientoResultadoExamenEmpleado.jsp';
	}
	else if(args.message.targetId=='MantenimientoResultadoExamenEmpleadoDocumento.jsp')
	{
		pagina='MantenimientoResultadoExamenEmpleadoDocumento.jsp';
	}	
	else if(args.message.targetId=='ListadoPorDia')
	{
		pagina='http://10.1.5.5/ReportServer/Pages/ReportViewer.aspx?%2fReportes+Alcohol+Drogas%2frptTestDiarioPersonal&rs:Command=Render';
	}
	else if(args.message.targetId=='ReportePorEmpleado')
	{
		pagina='http://10.1.5.5/ReportServer/Pages/ReportViewer.aspx?%2fReportes+Alcohol+Drogas%2frptTestPorTripulante&rs:Command=Render';
	}
	
	top.frames["mainFrame"].location.href=pagina;					
}						
</script>
   
<br>

   

</body>
</html>
