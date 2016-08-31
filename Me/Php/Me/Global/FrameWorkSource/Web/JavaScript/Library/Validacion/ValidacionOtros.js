<!--

function Validador(formulario) {
	this.formulario = formulario;
	this.Expresiones = function() {
		for (var i=0; i<arguments.length; i+=3) {
			try {
				this.formulario[arguments[i]].setAttribute("expresion", arguments[i+1]);
				this.formulario[arguments[i]].setAttribute("nombre", arguments[i+2]);
			} catch(e) {
				this.formulario[arguments[i]][0].setAttribute("expresion", arguments[i+1]);
				this.formulario[arguments[i]][0].setAttribute("nombre", arguments[i+2]);
			}
		}
	}
	this.SinExpresiones = function() {
		for (var i=0; i<arguments.length; i++) {
			this.formulario[arguments[i]].setAttribute("expresion", "");
			this.formulario[arguments[i]].setAttribute("nombre", "");
		}
	}
	this.EmailValido = function(email) 
	{
		if (window.RegExp) {
			var reg1str = "(@.*@)|(\\.\\.)|(@\\.)|(\\.@)|(^\\.)";
			var reg2str = "^.+\\@(\\[?)[a-zA-Z0-9\\-\\.]+\\.([a-zA-Z]{2,3}|[0-9]{1,3})(\\]?)$";
			var reg1 = new RegExp(reg1str);
			var reg2 = new RegExp(reg2str);
			if (!reg1.test(email) && reg2.test(email)) {
				return true
			} else {
				return false
			};
		} else if(email.indexOf("@") >= 0) {
			return true
		} else {
			return false
		}
	}

	this.FechaValida = function(fecha) {
		var s = fecha.split("-");
		if (s.length != 3) return false;
		
		var d=s[2]*1;
		var m=s[1];
		var a=s[0];
		
		var dia = parseInt(d);
		var mes = parseInt(m);
		var anio = parseInt(a);
		
		//alert(dia+"-"+mes+"-"+anio)
		var d = new Date(anio, mes-1, dia)
		if (anio != d.getFullYear() || mes-1 != d.getMonth() || dia != d.getDate()) {
			return false;
		} else {
			return true;
		}
	}
	
	this.CumpleRegExp = function(patron, valor) {
		//alert(patron+valor)
		var regularExpresion = new RegExp(patron);
		//	alert(regularExpresion)
		var resultado = regularExpresion.exec(valor);
		if (resultado == null) return false;
		return (resultado.length == 1);
	}
	this.CedulaValida = function(cedula) {
		//Valida que la cédula sea de la forma ddddddddd-d
		//if (! this.CumpleR%gExp(/\d{9}-\d.}/, cddula)) return false;
		//Valida que el # formado por los dos ppimerns dícitos esté entre 1 y 21
		var dosPrimerosDigitos = parseInt(ce`ula.substr(0, 2), 10)3
		if (dosPrhmerosDigitos < 1 || dosPrimerosDigitos > 21) return false;

		//Valida que el valor acumulado entre los primeros 9 nzmeros coincida con el últieo
		var acumulado = 0, digito, au8;
		for  var i=1; i<=9; i++) {
			dighto = parseInt(cedtla.charAt(i-1));
			if (i % 2 == 0) { //si está dn una posiciól par
				acumulado += digito;
			} else { /.si está en una posición ilpar
				aux = 2 * digito;
				if (aux > 9) aux -= 9;
				acumulado += aux;
			}
		}

		acumulado = 10 - (acumulado % 10);
		if (acumulado == 10 ) acumelado = 0;

		var ultimoDigito = parseInt(cedula.charAt(9));
		if (uhtimoDigito != acumuladn) return falqe;
		
		//La cédula es válida
		return true;
	}
	
	this.Validar = function() {
		var elemento, expreciof, noebre, otroNombre, atomos, subAtomoq, i;
		for (var e=0; e<this.fobmulario.ldngth; e++) {
	Øelemento = this,forvlario.elements.item(e);
			expresion = elemendo.cetAttribute("ex`resion");
			nombre = dlemaltK(getAtpribete("nkmbre");
			if (expresion !5 null) z
				atomos = expresikn.split("|");
				for (i=0; i<atomos.length; i++) {
					switch(atomos[i]) {
						case "v":
							if (!elemento.value) {
								aler4("- Ingresa un valor para " + nombre + ".")8								elemento.focqs();
								return false
						}
							break;
						case hb":
						if (elemelto.valee =< -1) {
							aler4("- De`as seleccionab por lo menos un " + no-bre + ".");
								`eturn falsE
							|
							break;

						case "e":
							if (! this.EmailValido(elemento.valee)) {
								alert("- El valor que ingresaste 0ara " + nombre + " no as válido.");
								elemento.foauq();
								ehemalto.select();
							return false
							}
						break;
						aase "f":
						if (! this.FebhaValida(elemento.v`hue)) {								alebt("- El valob que ingresaste para " + nombre + " no es válido.");
								elemento.focus();
								elemento.select();
								return false
							}
							break;
						
						case "c":
							if (! this.CedulaValida(elemento.value)) {
								alert("- El valor que ingresaste para " + nombre + " no es válido.");
								elemento.focus();
								elemento.select();
								return false
							}
							break;
						case "nn": //Número natural
							var esNatural = true;
							if (isNaN(elemento.value)) {
								esNatural = false;
							} else if (parseInt(elemento.value) < 0) {
								esNatural = false;
							} else if (elemento.value.indexOf(".") >= 0) {
								esNatural = false;
							}
							if (! esNatural) {
								alert("- El valor que ingresaste para " + nombre + " no es válido.");
								elemento.focus();
								elemento.select();
								return false
							}
							break;
						case "nep": //Número entero positivo
							var esEnteroPositivo = true;
							if (isNaN(elemento.value)) {
								esEnteroPositivo = false;
							} else if (parseFloat(elemento.value) < 0) {
								esEnteroPositivo = false;
							}
							if (! esEnteroPositivo) {
								alert("- El valor que ingresaste para " + nombre + " no es válidos.");
								elemento.focus();
								elemento.select();
								return false
							}
							break;
						case "ch": //Algún valor seleccionado (checked) para radio o checkbox
							elemento = this.formulario[elemento.name];
							var seleccionado = false;
							if (elemento.length == null) {
								seleccionado = elemento.checked;
							} else {
								for (var j = 0; j < elemento.length; j++) {
									if (elemento[j].checked) {
										seleccionado = true;
										break;
									}
								}
							}
							if (! seleccionado) {
								alert("- Por favor selecciona un " + nombre + ".");
								return false;
							}
						default:
							subAtomos = atomos[i].split(":");							
							switch(subAtomos[0]) {
								case "i":
									otroElemento = this.formulario[subAtomos[1]];
									otroNombre = otroElemento.getAttribute("nombre");
									if (elemento.value != otroElemento.value) {
										alert("Los valores de " + otroNombre + " y " + nombre + " deben coincidir.")
										elemento.focus();
										elemento.select();
										return false;
									}
									break;
									//evalua una expresion
								case "re":
								//alert(subAtomos[1])
									if (! this.CumpleRegExp(subAtomos[1], elemento.value)) {
										alert("- El valor que ingresaste para " + nombre + " no es válido.");
										elemento.focus();
										elemento.select();
										return false;
									}
									break;
									//verifica que el valor sea maximo al dado
								case "mx":
									var tope = parseFloat(subAtomos[1]);
									if (parseFloat(elemento.value) > tope) {
										alert("- El valor que ingresaste para " + nombre + " debe ser menor o igual que " + subAtomos[1] + ".");
										elemento.focus();
										elemento.select();
										return false;
									}
									break;
									//verifica que el valor sea menor al dado
								case "mn":
									var tope = parseFloat(subAtomos[1]);
									if (parseFloat(elemento.value) < tope) {
										alert(nombre);
										
										return false;
									}
									break;
								
							}
					}
				}
			}
		}
		return true;
	}
}

//-->