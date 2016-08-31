// ******************************************************************
// Valida el ingreso de campos tipo fecha formato valido ddmmyyyy
// Modo de uso:
//
//               onblur="check_date(this)"
//
// ******************************************************************


function check_date(field,req){
var mensaje="";

var checkstr = "0123456789";
var DateField = field;

if(field.value==""&&req=='s')
{
	 mensaje="Error: Campo requerido";
	 return mensaje;
}

var Datevalue = "";
var DateTemp = "";
var seperator = "/";
var day;
var month;
var year;
var leap = 0;
var err = 0;
var i;

   DateValue = DateField.value;
   /* Delete all chars except 0..9 */
   for (i = 0; i < DateValue.length; i++) {
	  if (checkstr.indexOf(DateValue.substr(i,1)) >= 0) {
	     DateTemp = DateTemp + DateValue.substr(i,1);
	  }
   }
   DateValue = DateTemp;
/* por: Gary Rivas */
/* Lee el mes y el anio actual */
/* para asimilar el GBS en el ingreso de fechas */

	fechahoy = new Date();
	Aniohoy  = fechahoy.getYear();
	Meshoy   = fechahoy.getMonth()+1;


	/* añade un cero a la izquierda cuando el mes es de un solo digito */
	if (("0"+ Meshoy).length == 2) 
		{ 
			Meshoy = "0" + Meshoy;
		}


   if (DateValue.length == 2) {

   DateValue = DateValue + Meshoy + Aniohoy ;

}

   if (DateValue.length == 4) {
   
   DateValue = DateValue + Aniohoy ;
   
}
   
   if (DateValue.length == 6) {

/* Si el año es mayor de 50 entonces utilice 1900 */
	   if (DateValue.substr(4,6) > 50) {anio=19;} else {anio=20;}   
	   
   DateValue = DateValue.substr(0,4) + anio + DateValue.substr(4,6); }
   
   if (DateValue.length != 8) {
      err = 19;}
   /* year is wrong if year = 0000 */
   year = DateValue.substr(4,4);
   if (year == 0) {
      err = 20;
   }
   /* Validation of month*/
   month = DateValue.substr(2,2);
   if ((month < 1) || (month > 12)) {
      err = 21;
   }
   /* Validation of day*/
   day = DateValue.substr(0,2);
   if (day < 1) {
     err = 22;
   }
   /* Validation leap-year / february / day */
   if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0)) {
      leap = 1;
   }
   if ((month == 2) && (leap == 1) && (day > 29)) {
      err = 23;
   }
   if ((month == 2) && (leap != 1) && (day > 28)) {
      err = 24;
   }
   /* Validation of other months */
   if ((day > 31) && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))) {
      err = 25;
   }
   if ((day > 30) && ((month == "04") || (month == "06") || (month == "09") || (month == "11"))) 
   {
      err = 26;
   }
   /* if 00 ist entered, no error, deleting the entry */
   if ((day == 0) && (month == 0) && (year == 00)) 
   {
      err = 0; day = ""; month = ""; year = ""; seperator = "";
   }
   /* if no error, write the completed date to Input-Field (e.g. 13.12.2001) */
   if (err == 0) 
   {
	   
      DateField.value = day + seperator +  month + seperator + year ;
   }
   /* Error-message if err != 0 */
   else {
	 
      mensaje=("Fecha incorrectaa!");
      /*DateField.select();
	  DateField.focus();*/
   }
   return mensaje;
}


function check_dateDate(date,req){
		
var mensaje="";

var checkstr = "0123456789";


if(date==""&&req=='s')
{
	 mensaje="Error: Campo requerido";
	 return mensaje;
}

var Datevalue = "";
var DateTemp = "";
var seperator = "/";
var day;
var month;
var year;
var leap = 0;
var err = 0;
var i;

	date=date.split(seperator)[2]+seperator+date.split(seperator)[1]+seperator+date.split(seperator)[0];
	
   DateValue = date;
   /* Delete all chars except 0..9 */
   for (i = 0; i < DateValue.length; i++) {
	  if (checkstr.indexOf(DateValue.substr(i,1)) >= 0) {
	     DateTemp = DateTemp + DateValue.substr(i,1);
	  }
   }
   DateValue = DateTemp;
/* por: Gary Rivas */
/* Lee el mes y el anio actual */
/* para asimilar el GBS en el ingreso de fechas */

	fechahoy = new Date();
	Aniohoy  = fechahoy.getYear();
	Meshoy   = fechahoy.getMonth()+1;


	/* añade un cero a la izquierda cuando el mes es de un solo digito */
	if (("0"+ Meshoy).length == 2) 
		{ 
			Meshoy = "0" + Meshoy;
		}


   if (DateValue.length == 2) {

   DateValue = DateValue + Meshoy + Aniohoy ;

}

   if (DateValue.length == 4) {
   
   DateValue = DateValue + Aniohoy ;
   
}
   
   if (DateValue.length == 6) {

/* Si el año es mayor de 50 entonces utilice 1900 */
	   if (DateValue.substr(4,6) > 50) {anio=19;} else {anio=20;}   
	   
   DateValue = DateValue.substr(0,4) + anio + DateValue.substr(4,6); }
   
   if (DateValue.length != 8) {
      err = 19;}
   /* year is wrong if year = 0000 */
   year = DateValue.substr(4,4);
   if (year == 0) {
      err = 20;
   }
   /* Validation of month*/
   month = DateValue.substr(2,2);
   
   if ((month < 1) || (month > 12)) {
      err = 21;
   }
   /* Validation of day*/
   day = DateValue.substr(0,2);
   if (day < 1) {
     err = 22;
   }
   /* Validation leap-year / february / day */
   if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0)) {
      leap = 1;
   }
   if ((month == 2) && (leap == 1) && (day > 29)) {
      err = 23;
   }
   if ((month == 2) && (leap != 1) && (day > 28)) {
      err = 24;
   }
   /* Validation of other months */
   if ((day > 31) && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))) {
      err = 25;
   }
   if ((day > 30) && ((month == "04") || (month == "06") || (month == "09") || (month == "11"))) {
      err = 26;
   }
   /* if 00 ist entered, no error, deleting the entry */
   if ((day == 0) && (month == 0) && (year == 00)) {
      err = 0; day = ""; month = ""; year = ""; seperator = "";
   }
   /* if no error, write the completed date to Input-Field (e.g. 13.12.2001) */
   if (err == 0) {
      date = day + seperator +  month + seperator + year ;
   }
   /* Error-message if err != 0 */
   else {
	   alert(err);
      mensaje=("Fecha incorrecta!");
      /*DateField.select();
	  DateField.focus();*/
   }
   return mensaje;
}
