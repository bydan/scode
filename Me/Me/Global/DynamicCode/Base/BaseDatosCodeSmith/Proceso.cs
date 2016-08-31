using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

public class Proceso {
	
	protected Int64 id;
	
	public Int64 getId() {
		return this.id;
	}
	
	public void setId(Int64 newId) {
		if(this.id!=newId) {
		}
		
		this.id=newId;
	}
	
	
	
	
	public Int64 id_grupo_proceso;
	
	public Int64 id_tipo_proceso;
	
	public String esquema;
	
	public String tabla;
	
	public Int64 id_tipo_proceso_columna;
	
	public String columna;
	
	public Int64 id_tipo_proceso_columnas;
	
	public String columnas;
	
	public Int32 orden;
	
	public Int32 sub_orden;
			
	
	public Proceso () : base() {			
		
		this.id=0L;
		
		
		this.id_grupo_proceso=-1L;
 		this.id_tipo_proceso=-1L;
 		this.esquema="";
 		this.tabla="";
 		this.id_tipo_proceso_columna=-1L;
 		this.columna="";
 		this.id_tipo_proceso_columnas=-1L;
 		this.columnas="";
 		this.orden=0;
 		this.sub_orden=0;
		
		
			
		
    } 
	
	
	//PROPERTIES
	
	public   Int64 idGrupoProceso_ { 
		get {
			return this.id_grupo_proceso;
		}
		set {
			this.setid_grupo_proceso(value);
			//this.id_grupo_proceso = value;
		}
	}
	
	public   Int64 idTipoProceso_ { 
		get {
			return this.id_tipo_proceso;
		}
		set {
			this.setid_tipo_proceso(value);
			//this.id_tipo_proceso = value;
		}
	}
	
	public   String Esquema_ { 
		get {
			return this.esquema;
		}
		set {
			this.setesquema(value);
			//this.esquema = value;
		}
	}
	
	public   String Tabla_ { 
		get {
			return this.tabla;
		}
		set {
			this.settabla(value);
			//this.tabla = value;
		}
	}
	
	public   Int64 idTipoProcesoColumna_ { 
		get {
			return this.id_tipo_proceso_columna;
		}
		set {
			this.setid_tipo_proceso_columna(value);
			//this.id_tipo_proceso_columna = value;
		}
	}
	
	public   String Columna_ { 
		get {
			return this.columna;
		}
		set {
			this.setcolumna(value);
			//this.columna = value;
		}
	}
	
	public   Int64 idTipoProcesoColumnas_ { 
		get {
			return this.id_tipo_proceso_columnas;
		}
		set {
			this.setid_tipo_proceso_columnas(value);
			//this.id_tipo_proceso_columnas = value;
		}
	}
	
	public   String Columnas_ { 
		get {
			return this.columnas;
		}
		set {
			this.setcolumnas(value);
			//this.columnas = value;
		}
	}
	
	public   Int32 Orden_ { 
		get {
			return this.orden;
		}
		set {
			this.setorden(value);
			//this.orden = value;
		}
	}
	
	public   Int32 SubOrden_ { 
		get {
			return this.sub_orden;
		}
		set {
			this.setsub_orden(value);
			//this.sub_orden = value;
		}
	}
		
	
    
	public Int64 getid_grupo_proceso() {
		return this.id_grupo_proceso;
	}
    
	public Int64 getid_tipo_proceso() {
		return this.id_tipo_proceso;
	}
    
	public String getesquema() {
		return this.esquema;
	}
    
	public String gettabla() {
		return this.tabla;
	}
    
	public Int64 getid_tipo_proceso_columna() {
		return this.id_tipo_proceso_columna;
	}
    
	public String getcolumna() {
		return this.columna;
	}
    
	public Int64 getid_tipo_proceso_columnas() {
		return this.id_tipo_proceso_columnas;
	}
    
	public String getcolumnas() {
		return this.columnas;
	}
    
	public Int32 getorden() {
		return this.orden;
	}
    
	public Int32 getsub_orden() {
		return this.sub_orden;
	}
	
    
	public void setid_grupo_proceso(Int64 newid_grupo_proceso)
	{
		try {
			if(this.id_grupo_proceso!=newid_grupo_proceso) {
				if(newid_grupo_proceso==null) {
					//newid_grupo_proceso=-1L;
					
				}

				this.id_grupo_proceso=newid_grupo_proceso;
			}
		} catch(Exception e) {
			throw e;
		}
	}
    
	public void setid_tipo_proceso(Int64 newid_tipo_proceso)
	{
		try {
			if(this.id_tipo_proceso!=newid_tipo_proceso) {
				if(newid_tipo_proceso==null) {
					//newid_tipo_proceso=-1L;
					
				}

				this.id_tipo_proceso=newid_tipo_proceso;
			}
		} catch(Exception e) {
			throw e;
		}
	}
    
	public void setesquema(String newesquema)
	{
		try {
			if(this.esquema!=newesquema) {
				if(newesquema==null) {
					//newesquema="";
				}

				if(newesquema!=null&&newesquema.Length>30) {
					newesquema=newesquema.Substring(0,28);
					//System.out.println("Proceso:Ha sobrepasado el numero maximo de caracteres permitidos,Maximo=30 en columna esquema");
				}

				this.esquema=newesquema;
			}
		} catch(Exception e) {
			throw e;
		}
	}
    
	public void settabla(String newtabla)
	{
		try {
			if(this.tabla!=newtabla) {
				if(newtabla==null) {
					//newtabla="";
				}

				if(newtabla!=null&&newtabla.Length>30) {
					newtabla=newtabla.Substring(0,28);
					//System.out.println("Proceso:Ha sobrepasado el numero maximo de caracteres permitidos,Maximo=30 en columna tabla");
				}

				this.tabla=newtabla;
			}
		} catch(Exception e) {
			throw e;
		}
	}
    
	public void setid_tipo_proceso_columna(Int64 newid_tipo_proceso_columna)
	{
		try {
			if(this.id_tipo_proceso_columna!=newid_tipo_proceso_columna) {
				if(newid_tipo_proceso_columna==null) {
					//newid_tipo_proceso_columna=-1L;
				}

				this.id_tipo_proceso_columna=newid_tipo_proceso_columna;
			}
		} catch(Exception e) {
			throw e;
		}
	}
    
	public void setcolumna(String newcolumna)
	{
		try {
			if(this.columna!=newcolumna) {
				if(newcolumna==null) {
					//newcolumna="";
				}

				if(newcolumna!=null&&newcolumna.Length>50) {
					newcolumna=newcolumna.Substring(0,48);
					//System.out.println("Proceso:Ha sobrepasado el numero maximo de caracteres permitidos,Maximo=50 en columna columna");
				}

				this.columna=newcolumna;
			}
		} catch(Exception e) {
			throw e;
		}
	}
    
	public void setid_tipo_proceso_columnas(Int64 newid_tipo_proceso_columnas)
	{
		try {
			if(this.id_tipo_proceso_columnas!=newid_tipo_proceso_columnas) {
				if(newid_tipo_proceso_columnas==null) {
					//newid_tipo_proceso_columnas=-1L;
				}

				this.id_tipo_proceso_columnas=newid_tipo_proceso_columnas;
			}
		} catch(Exception e) {
			throw e;
		}
	}
    
	public void setcolumnas(String newcolumnas)
	{
		try {
			if(this.columnas!=newcolumnas) {
				if(newcolumnas==null) {
					//newcolumnas="";
				}

				if(newcolumnas!=null&&newcolumnas.Length>200) {
					newcolumnas=newcolumnas.Substring(0,198);
					//System.out.println("Proceso:Ha sobrepasado el numero maximo de caracteres permitidos,Maximo=200 en columna columnas");
				}

				this.columnas=newcolumnas;
			}
		} catch(Exception e) {
			throw e;
		}
	}
    
	public void setorden(Int32 neworden)
	{
		try {
			if(this.orden!=neworden) {
				if(neworden==null) {
					//neworden=0;
				}

				this.orden=neworden;
			}
		} catch(Exception e) {
			throw e;
		}
	}
    
	public void setsub_orden(Int32 newsub_orden)
	{
		try {
			if(this.sub_orden!=newsub_orden) {
				if(newsub_orden==null) {
					//newsub_orden=0;
				}

				this.sub_orden=newsub_orden;
			}
		} catch(Exception e) {
			throw e;
		}
	}
	
	public List<Proceso> getProcesos(String sWhere,String sDataBase,String sHost,String sUser,String sPassword) {
		List<Proceso> procesos = new List<Proceso>();
		
		//using (SqlConnection connection = new SqlConnection("Data Source=(local);Initial Catalog="+sDataBase+";Integrated Security=SSPI"))
		
		
		String sSql="SELECT * FROM General.Proceso "+sWhere;
		
		//Trace.WriteLine(sSql);
		
		using (SqlConnection connection = new SqlConnection("Data Source="+sHost+";Initial Catalog="+sDataBase+";Integrated Security=False;Connect Timeout=30;User="+sUser+";Pwd="+sPassword+";"))
				
		using (SqlCommand cmd = new SqlCommand(sSql, connection))
		{
			connection.Open();
			
			using (SqlDataReader reader = cmd.ExecuteReader())
			{
				// Check is the reader has any rows at all before starting to read.
				if (reader.HasRows)
				{
					
					// Read advances to the next row.
					while (reader.Read())
					{
						Proceso proceso = new Proceso();
						// To avoid unexpected bugs access columns by name.
						proceso.id=reader.GetInt64(reader.GetOrdinal("id"));	
						proceso.id_grupo_proceso=-reader.GetInt64(reader.GetOrdinal("idGrupoProceso"));
						proceso.id_tipo_proceso=reader.GetInt64(reader.GetOrdinal("idTipoProceso"));
						proceso.esquema=reader.GetString(reader.GetOrdinal("Esquema"));
						proceso.tabla=reader.GetString(reader.GetOrdinal("Tabla"));
						proceso.id_tipo_proceso_columna=reader.GetInt64(reader.GetOrdinal("idTipoProcesoColumna"));
						proceso.columna=reader.GetString(reader.GetOrdinal("Columna"));
						proceso.id_tipo_proceso_columnas=reader.GetInt64(reader.GetOrdinal("idTipoProcesoColumnas"));
						proceso.columnas=reader.GetString(reader.GetOrdinal("Columnas"));
						proceso.orden=reader.GetInt32(reader.GetOrdinal("Orden"));
						proceso.sub_orden=reader.GetInt32(reader.GetOrdinal("SubOrden"));
						

						procesos.Add(proceso);
					}
				}
			}
		}
		
		return procesos;
	}
}