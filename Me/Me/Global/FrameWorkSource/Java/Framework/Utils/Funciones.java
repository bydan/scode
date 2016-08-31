package ByDan.Framework.Utils;
import java.util.*;
import java.text.DateFormat;
import java.text.SimpleDateFormat;

public class Funciones {

	public static String GetStringMySqlCurrentDateTime() 
	{
		Date date = new Date();
        
		String strDateTime="";
		DateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
	        
		strDateTime=dateFormat.format(date);
	     
		return strDateTime;
	}
	public static String GetStringAuditoriaAccion(AuditoriaAcciones auditoriaAccion) 
	{
		String strAccion="";
		  
		if(auditoriaAccion.equals(AuditoriaAcciones.ACTUALIZAR))
		{
			strAccion="Actualizar";
		}
		else if(auditoriaAccion.equals(AuditoriaAcciones.DEEPSAVE))
		{
			strAccion="DeepSave";
		}
		else if(auditoriaAccion.equals(AuditoriaAcciones.ELIMINARLOGICAMENTE))
		{
			strAccion="EliminarLogicamente";
		}
		else if(auditoriaAccion.equals(AuditoriaAcciones.ELIMINARFISICAMENTE))
		{
			strAccion="EliminarFisicamente";
		}
		else if(auditoriaAccion.equals(AuditoriaAcciones.NUEVO))
		{
			strAccion="Nuevo";
		}
		else if(auditoriaAccion.equals(AuditoriaAcciones.OTRO))
		{
			strAccion="Otro";
		}
		return strAccion;
	}
}
