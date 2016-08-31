package ByDan.Framework.DataAccess;

import java.util.*;
import java.sql.*;

import ByDan.Framework.Business.Entities.*;
import ByDan.Framework.Business.Logic.QueryWhereSelectParameters;
import ByDan.Framework.Utils.Connexion;
import ByDan.Framework.Utils.ParameterDbType;
import ByDan.Framework.Utils.ParameterMaintenance;
import ByDan.Framework.Utils.ParameterType;
import ByDan.Framework.Utils.ParametersMaintenance;
import ByDan.Framework.Utils.ParametersType;


public  class DataAccessHelper 
{
	public  static String BuildSqlGeneralGetEntities(GeneralEntity entity,QueryWhereSelectParameters queryWhereSelectParameters,String query)throws Exception
    {
        
        try {
        	
        	if(queryWhereSelectParameters.getFinalQuery().contains("INNER JOIN"))
			{
        		query=entity.getQuerySelect()+queryWhereSelectParameters.getFinalQuery()+ " AND Tema.isActive=1";  			
			}
			else
			{
				query=entity.getQuerySelect()+" WHERE Tema.isActive=1";
			}
						
			if(queryWhereSelectParameters.getQueryWhereSelectParameters()!="")
        	{
        		query+=" AND "+queryWhereSelectParameters.getQueryWhereSelectParameters();
        	}
			
			if(!queryWhereSelectParameters.getFinalQuery().contains("INNER JOIN"))
			{
				query= queryWhereSelectParameters.getInitialQuery()+query+queryWhereSelectParameters.getFinalQuery();
			}
    	 
    	 

    	} 
      catch(Exception ex)
      {
          throw new Exception();
      }
        return query;
    }
	
	public  static String BuildSqlGeneralGetEntities(GeneralEntity entity,QueryWhereSelectParameters queryWhereSelectParameters,String querySelect,String query)throws Exception
    {
        
        try {
        	
        	query=querySelect;
        	
        	if(queryWhereSelectParameters.getQueryWhereSelectParameters()!="")
        	{
        		query+=" WHERE "+queryWhereSelectParameters.getQueryWhereSelectParameters();
        	}
			
			if(!queryWhereSelectParameters.getFinalQuery().contains("INNER JOIN"))
			{
				query= queryWhereSelectParameters.getInitialQuery()+query+queryWhereSelectParameters.getFinalQuery();
			}
    	} 
      catch(Exception ex)
      {
          throw new Exception();
      }
        return query;
    }
	
	public  static Boolean EjecutarSql(String sql,Connexion con)throws SQLException,Exception
    {
        Boolean  ejecutado= true;
        try {
    	    Statement stmt = con.getConnection().createStatement();
    	    ejecutado= stmt.execute(sql);
    	   
    	    
    	    stmt.close();
    	 
    	 

    	} 
      catch(SQLException ex) 
      {
    	    throw new SQLException();
    	}
      catch(Exception ex)
      {
          throw new Exception();
      }
        return !ejecutado;
    }
	
	public static void Save(GeneralEntity entity,Connexion con)throws SQLException,Exception
     {		
		 try {
			 
		 if (entity.getIsDeleted())
         {
			 if (entity.getIsExpired())
	         {
				 entity.BuildParametersMaintenance(con.getDbType(),ParametersType.DELETE);
		 			
				 Delete(entity,con);
	         }
			 else 
	         {
				 entity.setIsActive(false);					
				 entity.BuildParametersMaintenance(con.getDbType(),ParametersType.UPDATE);
	      			
				 Update(entity,con);
				 entity.setIsChanged( false);
	         }
         }
         else if (entity.getIsChanged())
         {
        	
             if (entity.getIsNew())
             {
            	 entity.BuildParametersMaintenance(con.getDbType(),ParametersType.INSERT);
      			
            	 Insert( entity,con);
                 entity.setIsNew(false);
             }
             else
             {
            	 entity.BuildParametersMaintenance(con.getDbType(),ParametersType.UPDATE);
      			
                 Update(entity,con);
             }

             entity.setIsChanged( false);
         }
		 }
		 catch(SQLException ex)
		 {
			 
			 throw ex;


		 }
		 
		 catch(Exception ex)
	        {
	            throw ex;
	        }
		 finally 
		 {
		 }
		 }
     	
	private static void AddMYSQLParameter(Connexion con,PreparedStatement pstmt,ParameterMaintenance parameterMaintenance)throws SQLException
    {	
		if(parameterMaintenance.getParameterMaintenanceType().equals(ParameterType.BOOLEAN)){
			
			if(parameterMaintenance.getParameterMaintenanceValue().getValueObject()!=null)
			{
			pstmt.setBoolean(parameterMaintenance.getOrder(), parameterMaintenance.getParameterMaintenanceValue().getValueBoolean());
			}
			else
			{
				pstmt.setNull(parameterMaintenance.getOrder(), 0);
			}
			return;
		}
		
		if(parameterMaintenance.getParameterMaintenanceType().equals(ParameterType.BYTE))
		{
			if(parameterMaintenance.getParameterMaintenanceValue().getValueObject()!=null)
			{
			pstmt.setByte(parameterMaintenance.getOrder(), parameterMaintenance.getParameterMaintenanceValue().getValueByte());
			}
			else
			{
			pstmt.setNull(parameterMaintenance.getOrder(), 0);
			}
			return;
		}
		
		if(parameterMaintenance.getParameterMaintenanceType().equals(ParameterType.BYTES))
		{
			if(parameterMaintenance.getParameterMaintenanceValue().getValueObject()!=null)
			{
			pstmt.setBytes(parameterMaintenance.getOrder(), parameterMaintenance.getParameterMaintenanceValue().getValuebytes());
			}
			else
			{
			pstmt.setNull(parameterMaintenance.getOrder(), 0);
			}
			return;
		}
		
		if(parameterMaintenance.getParameterMaintenanceType().equals(ParameterType.DOUBLE))
		{
			if(parameterMaintenance.getParameterMaintenanceValue().getValueObject()!=null)
			{
			pstmt.setDouble(parameterMaintenance.getOrder(), parameterMaintenance.getParameterMaintenanceValue().getValueDouble());
			}
			else
			{
				pstmt.setNull(parameterMaintenance.getOrder(), 0);
			}
			return;
		}
			
		if(parameterMaintenance.getParameterMaintenanceType().equals(ParameterType.FLOAT))
		{
			if(parameterMaintenance.getParameterMaintenanceValue().getValueObject()!=null)
			{
			pstmt.setFloat(parameterMaintenance.getOrder(), parameterMaintenance.getParameterMaintenanceValue().getValueFloat());
			}
			else
			{
				pstmt.setNull(parameterMaintenance.getOrder(), 0);
			}
			return;
		}
		
		if(parameterMaintenance.getParameterMaintenanceType().equals(ParameterType.INT))
		{
			if(parameterMaintenance.getParameterMaintenanceValue().getValueObject()!=null)
			{
			pstmt.setInt(parameterMaintenance.getOrder(), parameterMaintenance.getParameterMaintenanceValue().getValueInteger());
			}
			else
			{
				pstmt.setNull(parameterMaintenance.getOrder(), 0);
			}
			return;
		}
		
		if(parameterMaintenance.getParameterMaintenanceType().equals(ParameterType.LONG))
		{
			if(parameterMaintenance.getParameterMaintenanceValue().getValueObject()!=null)
			{
			pstmt.setLong(parameterMaintenance.getOrder(), parameterMaintenance.getParameterMaintenanceValue().getValueLong());
			}
			else
			{
				pstmt.setNull(parameterMaintenance.getOrder(), 0);
			}
			return;
		}
		
		if(parameterMaintenance.getParameterMaintenanceType().equals(ParameterType.STRING))
		{
			if(parameterMaintenance.getParameterMaintenanceValue().getValueObject()!=null)
			{
			pstmt.setString(parameterMaintenance.getOrder(), parameterMaintenance.getParameterMaintenanceValue().getValueString());
			}
			else
			{
				pstmt.setNull(parameterMaintenance.getOrder(), 0);
			}
			return;
		}
		
		if(parameterMaintenance.getParameterMaintenanceType().equals(ParameterType.OBJECT))
		{
			if(parameterMaintenance.getParameterMaintenanceValue().getValueObject()!=null)
			{
			pstmt.setObject(parameterMaintenance.getOrder(), parameterMaintenance.getParameterMaintenanceValue().getValueObject());
			}
			else
			{
				pstmt.setNull(parameterMaintenance.getOrder(), 0);
			}
			return;
		}
		
		if(parameterMaintenance.getParameterMaintenanceType().equals(ParameterType.DATE))
		{
			if(parameterMaintenance.getParameterMaintenanceValue().getValueObject()!=null)
			{
				pstmt.setDate(parameterMaintenance.getOrder(),  parameterMaintenance.getParameterMaintenanceValue().getValueDate());
			} 
			else
			{
				pstmt.setNull(parameterMaintenance.getOrder(), 0);
			}
			return;
		}
		
		if(parameterMaintenance.getParameterMaintenanceType().equals(ParameterType.TIME))
		{
			if(parameterMaintenance.getParameterMaintenanceValue().getValueObject()!=null)
			{
				pstmt.setTime(parameterMaintenance.getOrder(),  parameterMaintenance.getParameterMaintenanceValue().getValueTime());
			} 
			else
			{
				pstmt.setNull(parameterMaintenance.getOrder(), 0);
			}
			return;
		}
		
		if(parameterMaintenance.getParameterMaintenanceType().equals(ParameterType.TIMESTAMP))
		{
			if(parameterMaintenance.getParameterMaintenanceValue().getValueObject()!=null)
			{
				pstmt.setTimestamp(parameterMaintenance.getOrder(),  parameterMaintenance.getParameterMaintenanceValue().getValueTimestamp());
			} 
			else
			{
				pstmt.setNull(parameterMaintenance.getOrder(), 0);
			}
			return;
		}
    }
	
	private static void Delete(GeneralEntity entity,Connexion con)throws SQLException,Exception
    {      
		   PreparedStatement pstmt = null;
		   Integer numeroRegistroModificado=0;
        try {
         
            pstmt = con.getConnection().prepareStatement(entity.getQueryDelete());        
            pstmt.setLong(1, entity.getId());
            
            
            	
            numeroRegistroModificado=pstmt.executeUpdate();
            
	            if(numeroRegistroModificado.equals(0))
	        	{
	        		throw new SQLException("No se actualizo los datos,intentelo nuevamente");		
	        	}
           
            
        }
        catch(SQLException ex)
        {
        	String s=ex.getMessage();
            throw new SQLException();
        }
        catch(Exception ex)
        {
            throw new Exception();
        }
        finally {
            if (pstmt != null) pstmt.close();
        }

    }
	
	private static void Insert(GeneralEntity entity,Connexion con)  throws SQLException,Exception {

    		PreparedStatement pstmt = null;
    		Integer numeroRegistroModificado=0;
    		Long id=0L;
    		try {
    		
    		pstmt = con.getConnection().prepareStatement(entity.getQueryInsert());
    		
    		for(ParameterMaintenance parameter:entity.getParametersMaintenance().getParametersMaintenance())
    		{
    			if(entity.getParametersMaintenance().getDbType().equals(ParameterDbType.MYSQL))
    			{
    			AddMYSQLParameter(con,pstmt,parameter);
    			}
    		
    		}
    		
    		numeroRegistroModificado=pstmt.executeUpdate();
    		
    		if(numeroRegistroModificado.equals(0))
    		{
    			throw new SQLException("No se actualizo los datos,intentelo nuevamente");		
    		}
    		entity.setParametersMaintenance(new ParametersMaintenance());
    		entity.setOrder(1);
    		
    		pstmt = con.getConnection().prepareStatement("select LAST_INSERT_ID();");
    		ResultSet rs =pstmt.executeQuery();
    		
    		   
       	    while (rs.next()) {
       		
       	    	id=rs.getLong("LAST_INSERT_ID()");      	    	
       	    }
       	    entity.setId(id);
    		}
    		catch(SQLException ex)
            {
    			String s=ex.getMessage();
                throw new SQLException();
            }
    		catch(Exception ex)
            {
                throw new Exception();
            }
    		finally {
    		if (pstmt != null) pstmt.close();
    		}
    		
    		}
      
	private static void Update(GeneralEntity entity,Connexion con)  throws SQLException,Exception {

	PreparedStatement pstmt = null;
	Integer numeroRegistroModificado=0;
	try {
	
	pstmt = con.getConnection().prepareStatement(entity.getQueryUpdate());
	
	for(ParameterMaintenance parameter:entity.getParametersMaintenance().getParametersMaintenance())
	{
		if(entity.getParametersMaintenance().getDbType().equals(ParameterDbType.MYSQL))
		{
		AddMYSQLParameter(con,pstmt,parameter);	
		}
	}

	numeroRegistroModificado=pstmt.executeUpdate();
	
	if(numeroRegistroModificado.equals(0))
	{
		throw new SQLException("No se actualizo los datos,intentelo nuevamente");		
	}
	
	entity.setParametersMaintenance(new ParametersMaintenance());
	entity.setOrder(1);

	}
	catch(SQLException ex)
    {
		String s=ex.getMessage();
        throw ex;
    }
	catch(Exception ex)
    {
        throw ex;
    }
	finally {
	if (pstmt != null) pstmt.close();
	}

	}
	
	public static class GetEntitiesDataAccessHelper<T extends GeneralEntity> {
	            
	public  T GetEntity(Connexion con, int id)throws SQLException,Exception
      {
          T entityT = null;
          try {
      	    Statement stmt = con.getConnection().createStatement();
      	    ResultSet rs = stmt.executeQuery(entityT.getQuerySelect()+ " WHERE "+GeneralEntity.getColumnNameIsActive()+"=1 AND "+GeneralEntity.getColumnNameId()+ "="+id);
      	   
      	    while (rs.next()) {
      		
      	    	entityT=GetEntity(entityT,rs);
      	    	
      	    }

      	    stmt.close();
      	    con.getConnection().close();

      	 

      	} 
        catch(SQLException ex) 
        {
      	    throw new SQLException();
      	}
        catch(Exception ex)
        {
            throw new Exception();
        }
          return entityT;
      }
	  
	public  ArrayList<T> GetEntities(GeneralEntity entity,QueryWhereSelectParameters queryWhereSelectParameters,Connexion con, int id) throws SQLException,Exception
      {
		  ArrayList<T> entitiesT = null;
		  T entityT = null;
		  
          try {
      	    Statement stmt = con.getConnection().createStatement();
      	    ResultSet rs = stmt.executeQuery(entity.getQuerySelect()+ "	WHERE "+GeneralEntity.getColumnNameIsActive()+"=1 AND"+queryWhereSelectParameters.getQueryWhereSelectParameters() +queryWhereSelectParameters.getFinalQuery());
      	   
      	    while (rs.next()) {
      	    	
      	    	entityT=GetEntity(entityT,rs);
      	    	entitiesT.add(entityT);
      	    }

      	    stmt.close();
      	    con.getConnection().close();

      	 

      	} 
        catch(SQLException ex) {
      	    throw new SQLException();
      	}
        catch(Exception ex)
        {
            throw new Exception();
        }
          return entitiesT;
      }
	  
	public  T GetEntity(T entityT,ResultSet rs) throws SQLException,Exception
      {
		 
		  entityT.setIsNew(false);
          try {
      	  
      	    	entityT.setId(rs.getLong(GeneralEntity.getColumnNameId()));	
      	    	entityT.setIsActive(rs.getBoolean(GeneralEntity.getColumnNameIsActive()));
      	    	entityT.setIsExpired(rs.getBoolean(GeneralEntity.getColumnNameIsExpired()));
      	    	entityT.setVersionRow(rs.getTimestamp(GeneralEntity.getColumnNameVersionRow()));     	    	       	 

      	} catch(SQLException ex) {
      	    throw new SQLException();
      	}
      	catch(Exception ex)
        {
            throw new Exception();
        }
          return entityT;
      }
	  }
	
	public static class GetEntitiesReadOnlyDataAccessHelper<T extends GeneralEntityReadOnly> {
        		  
		public  ArrayList<T> GetEntities(GeneralEntityReadOnly entity,QueryWhereSelectParameters queryWhereSelectParameters,Connexion con) throws SQLException,Exception
	      {
			  ArrayList<T> entitiesT = null;
			  T entityT = null;
			  
	          try {
	      	    Statement stmt = con.getConnection().createStatement();
	      	    ResultSet rs = stmt.executeQuery(entity.getQuerySelect()+ "	WHERE "+queryWhereSelectParameters.getQueryWhereSelectParameters() +queryWhereSelectParameters.getFinalQuery());
	      	   
	      	    while (rs.next()) {
	      	    	
	      	    	entityT=GetEntity(entityT,rs);
	      	    	entitiesT.add(entityT);
	      	    }

	      	    stmt.close();
	      	    con.getConnection().close();

	      	 

	      	} 
	        catch(SQLException ex) {
	      	    throw new SQLException();
	      	}
	        catch(Exception ex)
	        {
	            throw new Exception();
	        }
	          return entitiesT;
	      }
		  
		public  T GetEntity(T entityT,ResultSet rs) throws SQLException,Exception
	      {
			 
			  
	          try {
	      	  
	      	    	entityT.setId(rs.getInt(GeneralEntity.getColumnNameId()));	
   	    	       	 

	      	} catch(SQLException ex) {
	      	    throw new SQLException();
	      	}
	      	catch(Exception ex)
	        {
	            throw new Exception();
	        }
	          return entityT;
	      }
		  
	}
}
	  
