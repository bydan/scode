using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

public class GrupoProceso {	
	
	protected Int64 id;
		
	public Int64 getId() {
		return this.id;
	}
	
	public void setId(Int64 newId) {
		if(this.id!=newId) {
		
		}
		
		this.id=newId;
	}
	
	public Int64 id_sistema;
	public Int64 id_modulo;	
	public String codigo;	
	public String nombre;
				
		
	public GrupoProceso () {			
		
		this.id=0L;
		
		
		
		this.id_sistema=-1L;
 		this.id_modulo=-1L;
 		this.codigo="";
 		this.nombre="";
		
		
		
    } 
	
	
	//PROPERTIES
	
	public   Int64 idSistema_ { 
		get {
			return this.id_sistema;
		}
		set {
			this.setid_sistema(value);
			//this.id_sistema = value;
		}
	}
	
	public   Int64 idModulo_ { 
		get {
			return this.id_modulo;
		}
		set {
			this.setid_modulo(value);
			//this.id_modulo = value;
		}
	}
	
	public   String Codigo_ { 
		get {
			return this.codigo;
		}
		set {
			this.setcodigo(value);
			//this.codigo = value;
		}
	}
	
	public   String Nombre_ { 
		get {
			return this.nombre;
		}
		set {
			this.setnombre(value);
			//this.nombre = value;
		}
	}
		
	
    
	public Int64 getid_sistema() {
		return this.id_sistema;
	}
    
	public Int64 getid_modulo() {
		return this.id_modulo;
	}
    
	public String getcodigo() {
		return this.codigo;
	}
    
	public String getnombre() {
		return this.nombre;
	}
	
    
	public void setid_sistema(Int64 newid_sistema)
	{
		try {
			if(this.id_sistema!=newid_sistema) {
				if(newid_sistema==null) {
					
				}

				this.id_sistema=newid_sistema;
			}
		} catch(Exception e) {
			throw e;
		}
	}
    
	public void setid_modulo(Int64 newid_modulo)
	{
		try {
			if(this.id_modulo!=newid_modulo) {
				if(newid_modulo==null) {
					
				}

				this.id_modulo=newid_modulo;
				}
		} catch(Exception e) {
			throw e;
		}
	}
    
	public void setcodigo(String newcodigo)
	{
		try {
			if(this.codigo!=newcodigo) {
				if(newcodigo==null) {
					
				}

				if(newcodigo!=null&&newcodigo.Length>50) {
					newcodigo=newcodigo.Substring(0,48);
					//System.out.println("GrupoProceso:Ha sobrepasado el numero maximo de caracteres permitidos,Maximo=50 en columna codigo");
				}

				this.codigo=newcodigo;
			}
		} catch(Exception e) {
			throw e;
		}
	}
    
	public void setnombre(String newnombre)
	{
		try {
			if(this.nombre!=newnombre) {
				if(newnombre==null) {
					
				}

				if(newnombre!=null&&newnombre.Length>100) {
					newnombre=newnombre.Substring(0,98);
					//System.out.println("GrupoProceso:Ha sobrepasado el numero maximo de caracteres permitidos,Maximo=100 en columna nombre");
				}

				this.nombre=newnombre;
			}
		} catch(Exception e) {
			throw e;
		}
	}
	
	
	public List<GrupoProceso> getGruposProcesos(String sWhere,String sDataBase,String sHost,String sUser,String sPassword) {
		List<GrupoProceso> grupos_procesos = new List<GrupoProceso>();
		
		//using (SqlConnection connection = new SqlConnection("Data Source=(local);Initial Catalog="+sDataBase+";Integrated Security=SSPI"))
		
		String sSql="SELECT * FROM General.GrupoProceso "+sWhere;
		
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
						GrupoProceso grupo_proceso = new GrupoProceso();
						// To avoid unexpected bugs access columns by name.
						grupo_proceso.id = reader.GetInt64(reader.GetOrdinal("id"));
						grupo_proceso.id_sistema = reader.GetInt64(reader.GetOrdinal("idSistema"));
						grupo_proceso.id_modulo = reader.GetInt64(reader.GetOrdinal("idModulo"));
						grupo_proceso.codigo=reader.GetString(reader.GetOrdinal("Codigo"));
						grupo_proceso.nombre=reader.GetString(reader.GetOrdinal("Nombre"));
						
						grupos_procesos.Add(grupo_proceso);
						
						//int middleNameIndex = reader.GetOrdinal("MiddleName");
						// If a column is nullable always check for DBNull...
						/*
						if (!reader.IsDBNull(middleNameIndex))
						{
							p.MiddleName = reader.GetString(middleNameIndex);
						}
						*/
						


					}
				}
			}
		}
		
		return grupos_procesos;
	}
}