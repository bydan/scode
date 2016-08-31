package ByDan.Framework.Utils;

import ByDan.Framework.Utils.MensajeGrupo;
import ByDan.Framework.Utils.MensajeTipo;

public class Mensaje {
String strTitulo;
String strMensajeUsuario;
String strMensajeTecnico;
MensajeGrupo mensajeGrupo;
MensajeTipo mensajeTipo;
Boolean esOpcional;
String strNombreMensaje;

public Boolean getEsOpcional() {
	return esOpcional;
}

public void setEsOpcional(Boolean esOpcional) {
	this.esOpcional = esOpcional;
}

public Mensaje(String strNombreMensaje,String strTitulo,String strMensajeUsuario,String strMensajeTecnico,MensajeGrupo mensajeGrupo) 
{
	this.strNombreMensaje= strNombreMensaje;
	this.strTitulo=strTitulo;
	this.strMensajeUsuario=strMensajeUsuario;
	this.strMensajeTecnico=strMensajeTecnico;
	this.mensajeGrupo=mensajeGrupo;
	this.esOpcional=false;
	
	if(mensajeGrupo==MensajeGrupo.EXCEPTION)
	{
		this.mensajeTipo=MensajeTipo.ERROR;
		this.strNombreMensaje="excepcion";
	}
}

public Mensaje(String strTitulo,String strMensajeUsuario,String strMensajeTecnico,MensajeGrupo mensajeGrupo) 
{
	this.strTitulo=strTitulo;
	this.strMensajeUsuario=strMensajeUsuario;
	this.strMensajeTecnico=strMensajeTecnico;
	this.mensajeGrupo=mensajeGrupo;
	this.esOpcional=false;
	
	if(mensajeGrupo==MensajeGrupo.EXCEPTION)
	{
		this.mensajeTipo=MensajeTipo.ERROR;
		this.strNombreMensaje="excepcion";
	}
}

public Mensaje(String strNombreMensaje,String strTitulo,String strMensajeUsuario,String strMensajeTecnico,MensajeGrupo mensajeGrupo,MensajeTipo mensajeTipo,Boolean esOpcional) 
{
	this.strNombreMensaje=strNombreMensaje;
	this.strTitulo=strTitulo;
	this.strMensajeUsuario=strMensajeUsuario;
	this.strMensajeTecnico=strMensajeTecnico;
	this.mensajeGrupo=mensajeGrupo;
	this.mensajeTipo=mensajeTipo;
	this.esOpcional=esOpcional;
}

public Mensaje() {
	strTitulo="NONE";
	this.strMensajeUsuario="NONE";
	this.strMensajeTecnico="NONE";
	this.strNombreMensaje="NONE";
	mensajeGrupo=MensajeGrupo.APLICACION;
	mensajeTipo=MensajeTipo.INFORMACION;
	esOpcional=true;
}

public String toNewXmlMensaje() {
	StringBuffer xml = new StringBuffer();
	
	xml.append("<?xml version=\"1.0\"?>\n");
    xml.append("<mensaje generated=\""+System.currentTimeMillis()+"\">\n");	    
     
  	xml.append("<itemMensaje code=\""+System.currentTimeMillis()+"\">\n");
	xml.append("<grupo>");
	xml.append(mensajeGrupo);
	xml.append("</grupo>\n");

	xml.append("<tipo>");
	xml.append(mensajeTipo);
	xml.append("</tipo>\n");

	xml.append("<nombremensaje>");
	xml.append(this.strNombreMensaje);
	xml.append("</nombremensaje>\n");
	
	xml.append("<esopcional>");
	xml.append(esOpcional);
	xml.append("</esopcional>\n");
	
	xml.append("<titulo>");
	xml.append(strTitulo);
	xml.append("</titulo>\n");
	
	xml.append("<mensajeusuario>");
	xml.append(strMensajeUsuario);
	xml.append("</mensajeusuario>\n");
	
	xml.append("<mensajetecnico>");
	xml.append(strMensajeTecnico);
	xml.append("</mensajetecnico>\n");
	
	xml.append("</itemMensaje>\n");	
	
	xml.append("</mensaje>\n");
    
    	
  	
   
	return xml.toString();
}

public String toAppendXmlMensaje() {
StringBuffer xml = new StringBuffer();
	
     
  	xml.append("<itemMensaje code=\""+System.currentTimeMillis()+"\">\n");
  	
	xml.append("<grupo>");
	xml.append(mensajeGrupo);
	xml.append("</grupo>\n");

	xml.append("<tipo>");
	xml.append(mensajeTipo);
	xml.append("</tipo>\n");
	
	xml.append("<nombremensaje>");
	xml.append(this.strNombreMensaje);
	xml.append("</nombremensaje>\n");
	
	xml.append("<esopcional>");
	xml.append(esOpcional);
	xml.append("</esopcional>\n");
	
	xml.append("<titulo>");
	xml.append(strTitulo);
	xml.append("</titulo>\n");
	
	xml.append("<mensajeusuario>");
	xml.append(strMensajeUsuario);
	xml.append("</mensajeusuario>\n");
	
	xml.append("<mensajetecnico>");
	xml.append(strMensajeTecnico);
	xml.append("</mensajetecnico>\n");
	
	xml.append("</itemMensaje>\n");	    
    
    
	return xml.toString();
}

public static String getMensajeGeneralInformacionProcesoExitoso(String strNombre,String strTitulo,String strMensajeUsuario,String strMensajeTecnico) {
	Mensaje mensaje=new Mensaje(strNombre, strTitulo,strMensajeUsuario,strMensajeTecnico,MensajeGrupo.APLICACION,MensajeTipo.INFORMACION,false);	
	return mensaje.toNewXmlMensaje();	
}

public static String getMensajeGeneraldefault() {
	Mensaje mensaje=new Mensaje("NONE","NONE","NONE","NONE",MensajeGrupo.NONE,MensajeTipo.NONE,true);	
	return mensaje.toAppendXmlMensaje();	
}

public static String getMensajeGeneralNoExisteBusqueda(String nombreClase) {
	Mensaje mensaje=new Mensaje("noexistencia","Busqueda de "+nombreClase+"s","No existen "+nombreClase+"s en esta busqueda","NONE",MensajeGrupo.APLICACION,MensajeTipo.INFORMACION,true);	
	return mensaje.toNewXmlMensaje();	
}

public static String getMensajeGeneralNuevo(String nombreClase) {
	Mensaje mensaje=new Mensaje("nuevo","Mantenimiento de "+nombreClase+"","Nuevo "+nombreClase+" ingresado correctamente","NONE",MensajeGrupo.APLICACION,MensajeTipo.INFORMACION,true);	
	return mensaje.toAppendXmlMensaje();	
}

public static String getMensajeGeneralActualizar(String nombreClase) {
	Mensaje mensaje=new Mensaje("actualizar","Mantenimiento de "+nombreClase+"",nombreClase+" actualizado correctamente","NONE",MensajeGrupo.APLICACION,MensajeTipo.INFORMACION,true);	
	return mensaje.toAppendXmlMensaje();	
}

public static String getMensajeGeneralEliminar(String nombreClase) {
	Mensaje mensaje=new Mensaje("eliminar","Mantenimiento de "+nombreClase+"",nombreClase+" eliminado correctamente","NONE",MensajeGrupo.APLICACION,MensajeTipo.INFORMACION,true);	
	return mensaje.toAppendXmlMensaje();	
}

public static String getMensajeGeneralDeepSave(String titulo) {
	Mensaje mensaje=new Mensaje("deepsave",titulo,"Los datos han sido guardados correctamente","NONE",MensajeGrupo.APLICACION,MensajeTipo.INFORMACION,false);	
	return mensaje.toAppendXmlMensaje();	
}

public static String getMensajeGeneralDeepLoad(String titulo) {
	Mensaje mensaje=new Mensaje("deepload",titulo,"Los datos han sido cargados correctamente","NONE",MensajeGrupo.APLICACION,MensajeTipo.INFORMACION,true);	
	return mensaje.toAppendXmlMensaje();	
}

public MensajeGrupo getMensajeGrupo() {
	return mensajeGrupo;
}
public void setMensajeGrupo(MensajeGrupo mensajeGrupo) {
	this.mensajeGrupo = mensajeGrupo;
}
public MensajeTipo getMensajeTipo() {
	return mensajeTipo;
}
public void setMensajeTipo(MensajeTipo mensajeTipo) {
	this.mensajeTipo = mensajeTipo;
}

public String getStrMensajeTecnico() {
	return strMensajeTecnico;
}

public void setStrMensajeTecnico(String strMensajeTecnico) {
	this.strMensajeTecnico = strMensajeTecnico;
}

public String getStrMensajeUsuario() {
	return strMensajeUsuario;
}

public void setStrMensajeUsuario(String strMensajeUsuario) {
	this.strMensajeUsuario = strMensajeUsuario;
}

public String getStrTitulo() {
	return strTitulo;
}
public void setStrTitulo(String strTitulo) {
	this.strTitulo = strTitulo;
}

public String getStrNombreMensaje() {
	return strNombreMensaje;
}

public void setStrNombreMensaje(String strNombreMensaje) {
	this.strNombreMensaje = strNombreMensaje;
}
}
