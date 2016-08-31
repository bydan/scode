// ******************************************************************
// Transforma a format ###,###,###.00 cualquier numero ingresado
// devuelve 0.00 cuando no es un numero valido
//
// ******************************************************************


function format_num(num) {
num = num.toString().replace(/\$|\,/g,'');
if(isNaN(num))
num = "0";
sign = (num == (num = Math.abs(num)));
num = Math.floor(num*100+0.50000000001);
cents = num%100;
num = Math.floor(num/100).toString();
if(cents<10)
cents = "0" + cents;
for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
num = num.substring(0,num.length-(4*i+3))+','+
num.substring(num.length-(4*i+3));
return (((sign)?'':'-') + '' + num + '.' + cents);
}


//HOLGER Función que integra la anterior función format_num, pero viendo si quiero que valide 1 decimal o un entero
//D = Decimal; I = Entero

function format_numDI(num, tipo) {

if(tipo=="D")
{
	return format_num(num)
}
else if (tipo=="I")
{
	if(isNaN(num))
		return 0;
	else if(num.indexOf('.') > 0 ) 
		return 0;
	else 
		return num;
}
else
	return 0;

}



