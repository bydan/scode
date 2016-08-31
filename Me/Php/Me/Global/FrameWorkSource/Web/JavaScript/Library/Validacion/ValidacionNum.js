// ******************************************************************
// Valida el ingreso de campos numericos, se especifica rango valido
// Modo de uso:
//
//               onblur="check_num(this,minimo,maximo)"
//
// ******************************************************************


function check_num(field,minimo,maximo,dec,req){
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
   DateValue = DateField.value;
   var anum=/(^\d+$)|(^\d+\.\d+$)/

if (DateValue.length == 0) {
	return 
}	

	if (anum.test(DateValue)) {
		err=0
	} else {
		err=10
		mensaje=("Error: Ingrese solo numeros!");
		}

	if (dec=='n') {
		if ((DateValue.indexOf('.')) > 0 ) {
			err=12
			mensaje=("Error: Solo ingrese valores enteros")
		}
	
	}

	if ((DateValue < minimo) && (err ==0)) {
		err=15
		mensaje=("Error: Numero ingresado no debe ser menor de "+ minimo);
	}
		
	if ((DateValue > maximo) && (err ==0)) {
		err=20
		mensaje=("Error: Numero ingresado no debe mayor a "+ maximo);
	}
		
	if (err > 0) {
		;
		
		//DateField.select();
		//DateField.focus();
	}
return mensaje;
}
