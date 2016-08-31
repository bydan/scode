
function trim(cadena)
{
	for(i=0; i<cadena.length; )
	{
		if(cadena.charAt(i)==" ")
			cadena=cadena.substring(i+1, cadena.length);
		else
			break;
	}

	for(i=cadena.length-1; i>=0; i=cadena.length-1)
	{
		if(cadena.charAt(i)==" ")
			cadena=cadena.substring(0,i);
		else
			break;
	}
	
	return cadena;
}

function DeshabilitarHabilitarControlesTodosOUno(formulario,nombreControl,esHabilitar) 
{
	for (var i=0;i<formulario.elements.length;i++)
	{
		if(formulario.elements[i].type=="text"||formulario.elements[i].type=="textarea")
		{
			if(formulario.elements[i].name=="")
			{			
				if(esHabilitar==false)
				{
					formulario.elements[i].readOnly=true;
				}
				else
				{
					formulario.elements[i].readOnly=false;
				}
			}
			else
			{
				if(formulario.elements[i].name==nombreControl)
				{
					if(esHabilitar==false)
					{
						formulario.elements[i].readOnly=true;
					}
					else
					{
						formulario.elements[i].readOnly=false;
					}
				}
				else
				{
					if(esHabilitar==false)
					{
						formulario.elements[i].readOnly=false;
					}
					else
					{					
						formulario.elements[i].readOnly=true;
					}
				}	
			}
		}
	}	
}

function VerificarPermisosMantenimiento(nombrepagina)
{
	
 var req = newXMLHttpRequest();

 req.onreadystatechange = getReadyStateHandler(req, ProcesarPermisosMantenimiento);
 
 req.open("POST", "UsuarioServletAdditional", true);
 req.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
 
 req.send("accion=verificarpermisosmantenimiento&nombrepagina="+nombrepagina);
 
}


function AsignarPermisosAcciones(permisos) 
{
	var items=permisos.split(",");
	
	for (var I = 0 ; I < items.length ; I++) 
   {
	    var item = items[I];
		
		if(item=="t")
		{
			document.getElementById("btnNuevo").style.visibility="visible";
			document.getElementById("btnActualizar").style.visibility="visible";
			document.getElementById("btnEliminar").style.visibility="visible";
			document.getElementById("btnCancelar").style.visibility="visible";
			break;
		}
		
		if(item=="gn")
		{
			document.getElementById("btnNuevo").style.visibility="visible";
			document.getElementById("btnCancelar").style.visibility="visible";
		}
		if(item=="ga")
		{		
			document.getElementById("btnActualizar").style.visibility="visible";
			document.getElementById("btnCancelar").style.visibility="visible";
		}
		if(item=="ge")
		{
			document.getElementById("btnEliminar").style.visibility="visible";
			document.getElementById("btnCancelar").style.visibility="visible";
		}
		
   }
}

function ProcesarPermisosMantenimiento(validacionusuarioXML) 
{
validacionusuarioXML.getElementsByTagName("mensaje")[0]
var mensaje = validacionusuarioXML.getElementsByTagName("mensaje")[0];	
var itemMensaje = mensaje.getElementsByTagName("itemMensaje");
	
if(itemMensaje!=null)
{
	var nombremensaje= itemMensaje[0].getElementsByTagName("nombremensaje")[0].firstChild.nodeValue;
	
	if(nombremensaje!="verificarpermisosmantenimiento")
	{
		ProcesarMensaje(itemMensaje);
		return;
	}
	
	var mensajeusuario= itemMensaje[0].getElementsByTagName("mensajeusuario")[0].firstChild.nodeValue;
	var tipo= itemMensaje[0].getElementsByTagName("tipo")[0].firstChild.nodeValue;
	var esopcional= itemMensaje[0].getElementsByTagName("esopcional")[0].firstChild.nodeValue;
	var titulo= itemMensaje[0].getElementsByTagName("titulo")[0].firstChild.nodeValue;	
	var mensajetecnico; 
	
	if(itemMensaje[0].getElementsByTagName("mensajetecnico")[0].firstChild.nodeValue!="NONE")
	{
			mensajetecnico=itemMensaje[0].getElementsByTagName("mensajetecnico")[0].firstChild.nodeValue;
	}
	
	
	if(mensajetecnico!="NONE")
	{
		if(mensajetecnico=="true")
		{
			AsignarPermisosAcciones(mensajeusuario);
		}
		else
		{
		 createSimpleYahooDialogInfo(titulo,mensajeusuario,YAHOO.widget.SimpleDialog.ICON_BLOCK);
		}
	}
}
}

function CambiarNullAVacio(valor) 
{
	var valorfinal=valor;
	
	if(valor=="null"||valor=="0")
	{
		valorfinal="";
	}
	
return valorfinal;
}

function CambiarNullAVacioSoloNull(valor) 
{
	var valorfinal=valor;
	
	if(valor=="null")
	{
		valorfinal="";
	}
	
return valorfinal;
}

function CambiarBooleanValueToControl(booleanValue,id) 
{
	var control="<input name=\"chb"+id+"\" type=\"checkbox\" disabled=\"true\" ";
	if(booleanValue=="true")
	{
		control+="checked>"
	}
	else
	{
		control+=">"
	}
return control;
}

function CambiarBooleanValueToControlHabilitadoActivarEmpleado(booleanValue,id) 
{
	var control="<input name=\"chb"+id+"\" type=\"checkbox\"" +"onClick=\"SeleccionarDeseleccionarEmpleado(this,"+id+")\"";
	if(booleanValue==true)
	{
		control+="checked>"
	}
	else
	{
		control+=">"
	}
	
return control;
}

function CambiarBooleanValueToControlHabilitadoActivarEmpleadoTraerDatos(booleanValue,id) 
{
	var control="<input name=\"chb"+id+"\" type=\"checkbox\"" +"onClick=\"SeleccionarDeseleccionarEmpleado(this,"+id+")\"";
	if(booleanValue=="true")
	{
		control+="checked>"
	}
	else
	{
		control+=">"
	}
	
return control;
}

function CambiarBooleanValueToControl2(booleanValue,id) 
{
	var control="<input name=\"chb"+id+"\" type=\"checkbox\" disabled=\"true\"";
	if(booleanValue==true)
	{
		control+=" checked>"
	}
	else
	{
		control+=">"
	}
return control;
}

function CambiarBooleanNumeroValueToControl(booleanValue,id) 
{
	var control="<input name=\"chb"+id+"\" type=\"checkbox\" disabled=\"true\" ";
	if(booleanValue=="1")
	{
		control+=" checked>"
	}
	else
	{
		control+=">"
	}
return control;
}
function CambiarFechaHoraAFecha(fechahora) 
{
	var fecha=fechahora.split(" ");
	
return fecha[0];
}