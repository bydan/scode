// ******************************************************************
// Valida el ingreso de campos tipo texto o generales
// Modo de uso:
//
//      onblur="check_text(this,longitud,sizefijo,conv,dig,let,esp)"
//
// ******************************************************************

function check_text_tipo_sangre(field,longitud,sizefijo,conv, dig,let,esp,req){

var mensaje="";
var DateField = field;

if(field.value==""&&req=='s')
{
	 mensaje="Error: Campo requerido";
	 return mensaje;
}	

var err = 0;
var i;
err = 0;

var digitos ='0123456789';								// Digitos
var letrasMin ='abcdefghijklmnopqrstuvwxyz';			// áéíóú Letras minusculas
var letrasMay ='ABCDEFGHIJKLMNOPQRSTUVWXYZ';			// ÁÉÍÓÚ Letras mayusculas
var espacios = '.,:\\ \t\n\r+-';								// whitespace characters

var caractpermitidos='';
var permite=' ';

DateValue = DateField.value;


if (dig=='s') {
	caractpermitidos=caractpermitidos+digitos;
	permite = "numeros ";
}
if (let=='s') {
	caractpermitidos=caractpermitidos+letrasMin+letrasMay;
	permite = permite + " letras";
}
if (esp=='s') {
	caractpermitidos=caractpermitidos+espacios;
	permite = permite + " espacios";
}

if(!valContenido(DateValue,caractpermitidos)){
		mensaje=('Error: Debe Ingresar solo: '+ permite);
		err = 10
}

		
if ((DateValue.length > longitud) && (err ==0)) {
		err=20
		mensaje=("Error: Solo se puede ingresar "+ longitud+ " caracteres" );
	}

if ((DateValue.length < longitud) && (err ==0) &&  (sizefijo == 's')) {
		err=15
		mensaje=("Error: El campo ingresado debe contener "+ longitud+ " caracteres" );
}

if (conv=='s'){
	DateField.value = DateField.value.toUpperCase();
}
		
if (err > 0) {
	;	
		//DateField.select();
		//DateField.focus();		
}
return mensaje;
}

function check_text(field,longitud,sizefijo,conv, dig,let,esp,req){
var mensaje="";
var DateField = field;

if(field.value==""&&req=='s')
{
	 mensaje="Error: Campo requerido";
	 return mensaje;
}	

var err = 0;
var i;
err = 0;

var digitos ='0123456789';								// Digitos
var letrasMin ='abcdefghijklmnopqrstuvwxyz';			// áéíóú Letras minusculas
var letrasMay ='ABCDEFGHIJKLMNOPQRSTUVWXYZ';			// ÁÉÍÓÚ Letras mayusculas
var espacios = '.,:\\ \t\n\r';								// whitespace characters

var caractpermitidos='';
var permite=' ';

DateValue = DateField.value;


if (dig=='s') {
	caractpermitidos=caractpermitidos+digitos;
	permite = "numeros ";
}
if (let=='s') {
	caractpermitidos=caractpermitidos+letrasMin+letrasMay;
	permite = permite + " letras";
}
if (esp=='s') {
	caractpermitidos=caractpermitidos+espacios;
	permite = permite + " espacios";
}

if(!valContenido(DateValue,caractpermitidos)){
		mensaje=('Error: Debe Ingresar solo: '+ permite);
		err = 10
}

		
if ((DateValue.length > longitud) && (err ==0)) {
		err=20
		mensaje=("Error: Solo se puede ingresar "+ longitud+ " caracteres" );
	}

if ((DateValue.length < longitud) && (err ==0) &&  (sizefijo == 's')) {
		err=15
		mensaje=("Error: El campo ingresado debe contener "+ longitud+ " caracteres" );
}

if (conv=='s'){
	DateField.value = DateField.value.toUpperCase();
}
		
if (err > 0) {
	;	
		//DateField.select();
		//DateField.focus();		
}
return mensaje;
}

function valContenido(str,con){
    var i;

    for (i = 0; i < str.length; i++){   
        var c = str.charAt(i);
        if (con.indexOf(c) == -1) return false;
    }
    return true;
}