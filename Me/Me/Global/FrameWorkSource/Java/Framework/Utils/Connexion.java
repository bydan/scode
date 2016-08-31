package ByDan.Framework.Utils;

import java.sql.*;
import com.mysql.jdbc.Driver;


public class Connexion {

private	String driver;
private	String schema;
private	String user;
private	String password;
private	ParameterDbType dbType;
private	ConnexionType connexionType;
private java.sql.Connection connection;


public Connexion() {
	
}
public Connexion(String newDriver,String newSchema,String newUser,String newPassword,ParameterDbType newDbType) throws SQLException,Exception
{
	try
	{	
	 driver=newDriver;
	 schema=newSchema;
	 user=newUser;
	 password=newPassword;
	 dbType=newDbType;
	 connection=DriverManager.getConnection(driver);
	}
	catch(SQLException ex) 
	{
		throw new SQLException();
  	}
	catch(Exception ex) 
	{
		throw new Exception();
  	}
}
public Connexion(String newDriver,ConnexionType newConnexionType,ParameterDbType newDbType) throws SQLException,Exception
{
	 String strDriver="";
	 try
		{	 
	 dbType=newDbType;
	 connexionType=newConnexionType;
	 
	 if(connexionType==ConnexionType.ODBC)
	 {
		 strDriver="jdbc:odbc:";
	 }
	 Driver driverMySql= new Driver();
	 
	 driver= strDriver+newDriver;
	 DriverManager.registerDriver(driverMySql);
	 connection=DriverManager.getConnection(driver);
	 connection.setAutoCommit(false);
	}
		catch(SQLException ex) 
		{
			throw new SQLException();
	  	}
		catch(Exception ex) 
		{
			throw new Exception();
	  	}
}

public static Connexion getNewSeguridadTestConnexion()throws SQLException,Exception 
{
	Connexion connexion= new Connexion();
	try
	{
	connexion= new Connexion("MySqlOdbcTest",ConnexionType.ODBC,ParameterDbType.MYSQL);
	}
	catch(SQLException ex) 
	{
		throw new SQLException();
  	}
	catch(Exception ex) 
	{
		throw new Exception();
  	}
 	return connexion;
 	
}


public static Connexion getNewConnexion()throws SQLException,Exception 
{
	Connexion connexion= new Connexion();
	String strDriverJdbc32MySql= "jdbc:mysql://localhost/asambleavirtual_dbo?" +  "user=root&password=root105";
	try
	{
		//connexion= new Connexion("odbcAsamblea",ConnexionType.ODBC,ParameterDbType.MYSQL);
		connexion= new Connexion(strDriverJdbc32MySql,ConnexionType.JDBC32,ParameterDbType.MYSQL);
	}
	catch(SQLException ex) 
	{
		throw new SQLException();
  	}
	catch(Exception ex) 
	{
		throw new Exception();
  	}
 	return connexion;
 	
}
public static Connexion getNewConnexionAuditoria()throws SQLException,Exception 
{
	Connexion connexion= new Connexion();
	String strDriverJdbc32MySql= "jdbc:mysql://localhost/asambleavirtualauditoria_dbo?" +  "user=root&password=";
	try
	{
		//connexion= new Connexion("odbcAsamblea",ConnexionType.ODBC,ParameterDbType.MYSQL);
		connexion= new Connexion(strDriverJdbc32MySql,ConnexionType.JDBC32,ParameterDbType.MYSQL);
	}
	catch(SQLException ex) 
	{
		throw new SQLException();
  	}
	catch(Exception ex) 
	{
		throw new Exception();
  	}
 	return connexion;
 	
}
public String getSchema() {
	return schema;
}
public void setSchema(String newSchema) {
	this.schema = newSchema;
}
public java.sql.Connection getConnection() {
	return connection;
}
public void setConnection(java.sql.Connection newConnection) {
	this.connection = newConnection;
}

public String getDriver() {
	return driver;
}
public void setDriver(String newDriver) {
	this.driver = newDriver;
}
public String getPassword() {
	return password;
}
public void setPassword(String newPassword) {
	this.password = newPassword;
}
public String getUser() {
	return user;
}
public void setUser(String newUser) {
	this.user = newUser;
}
public ParameterDbType getDbType() {
	return dbType;
}
public void setDbType(ParameterDbType tipo) {
	this.dbType = tipo;
}
}
