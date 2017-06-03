#import matplotlib.pyplot as plt
#import numpy

def subtermino(termino):
    salida = ""
    j = termino.find('(')
    fx = termino[0:j]
    i = termino.find(')', j+1)
    gx = termino[j+1:i]
    return fx, gx

def fraccion_to_decimal(fraccion):
    primero = fraccion[0:fraccion.find('/')]
    segundo = fraccion[fraccion.find('/')+1:]
    return float(primero) / float(segundo)

def regla_cadena(fx, gx):
    salida = ''
    dgx = derivar_termino(*separar(gx))
    if fx == 'cos' or fx == '+cos':
        salida += '-sen(' + str(gx) + ')' + ' * ' + str(eval(dgx)) 
    elif fx == '-cos':
        salida += 'sen(' + str(gx) + ')' + ' * ' + str(eval(dgx))
    elif fx == 'sen' or fx == '+sen':
        salida += 'cos(' + str(gx) + ')' + ' * ' + str(eval(dgx))
    elif fx == '-sen':
        salida += '-cos(' + str(gx) + ')' + ' * ' + str(eval(dgx))
    return salida

def separar(termino):
    fin_coeficiente = len(termino)
    es_positiva = True
    if termino[0] == '-':
        es_positiva = False
        inicio = 1
    elif termino[0] == '+':
        es_positiva = True
        inicio = 1
    else:
        inicio = 0
    if termino == 'x':
        coeficiente = 1
    else:
        # 2x
        # 10x
        # 2x^2
        # -x
        # -2x
        # x^(1/2)
        # 2^5
        for i in range(inicio, len(termino)):
            if termino[i] > '9' or termino[i] < '0' and termino[i] != '/':
                fin_coeficiente = i 
                break
        # 10x
        # -10x
        if fin_coeficiente <= 0:
            c = termino[inicio:inicio + 1]
        else:
            c = termino[inicio:fin_coeficiente]
        if c.find('/') >= 0:
            coeficiente = fraccion_to_decimal(c)
        elif c == 'x' or len(c) == 0:
            coeficiente = 1
        else:
            coeficiente = int(c)
        
    
    # if fin_coeficiente != -1:
    #     if fin_coeficiente+1 != inicio:
    #         coeficiente = int(termino[inicio:fin_coeficiente+1])
    #     else:
    #         coeficiente = termino[inicio:fin_coeficiente+2]
    #         if coeficiente != 'x':
    #             coeficiente = int(coeficiente)
    #         else:
    #             # termino independiente x
    #             coeficiente = 1
    if termino.find('^') >= 0 and  termino.find('(') < 0 and termino.find('x') >= 0 and termino.find('/') < 0:
        potencia = int(termino[termino.find('^') + 1:])
    elif termino.find('^') >= 0 and  termino.find('(') < 0 and termino.find('x') >= 0:
        potencia = float(termino[termino.find('^')+1:termino.find('/')]) / float(termino[termino.find('/')+1:])
    elif termino.find('x') >= 0 and termino.find('^') < 0:
        potencia = 1
    elif termino.find('(') >= 0 and termino.find('x') >= 0:
        fraccion = termino[termino.find('(')+1:termino.find(')')]
        potencia = fraccion_to_decimal(fraccion)
    else:
        potencia = 0
    return coeficiente, potencia, es_positiva

def derivar(funcion):
    funcion = formatear_simple(limpiar_blancos(funcion))
    salida = ""
    terminos = funcion.split()
    for t in terminos:
        if t.find('s') > 0:
            salida += regla_cadena(*subtermino(t)) + " "
        else:
            dx = derivar_termino(*separar(t))
            salida +=  dx
    if not salida.strip():
        return 0
    # para darle formato a la salida
    if salida[1] == '+':
        return salida[3:]
    elif salida[1] == '-':
        return salida[1] + salida[3:]
    elif salida[0] == ' ':
        return salida[1:]
    return salida
    
def derivar_termino(coeficiente, potencia, es_positiva):
    salida = ""
    if not es_positiva:
        if potencia < 0:
            salida = " + "
        else:
            salida = " - "
    elif potencia >= 0:
        salida = " + "
    else:
        salida = ' - '
    t = abs(coeficiente * potencia)
    if potencia == 1:
        return salida + str(t)
    elif potencia == 2:
        return salida + str(t) + "x" 
    elif potencia == 0:
        return ""
    elif t != 1:        
        return salida + str(t) + "x^" + str((potencia - 1))
    else:
        return salida + "x^" + str((potencia - 1))
def formatear(funcion):
    # (x^3+2)/3
    # 'cos(2x)^3+35x'
    # cos(2x)
    salida = ""
    comienza_parentesis = False
    if funcion.startswith("("):
        comienza_parentesis = True
    j = 0
    for i in range(0, len(funcion)):
        if funcion[i] in ['-', '+', ')']:
            if not funcion[i-1] in ['^', '('] and not comienza_parentesis: 
                try:
                    if funcion[i+1] != '^':
                        if funcion[i] == ')':
                            salida += funcion[j:i+1] + " "
                            j = i + 1
                        else:
                            salida += funcion[j:i] + " "
                            j = i
                except IndexError:
                    pass
            elif funcion[i] == ')':
                salida += funcion[j:i+1] + " "
                j = i + 1
    salida += funcion[j:]
    return salida

def formatear_simple(funcion):
    # 3x^2+10x+5
    salida = ""
    j = 0
    for i in range(0, len(funcion)):
        if funcion[i] in ['+', '-'] and i > 0:
            if funcion[i-1] != '^':
                salida += funcion[j:i] + " "
                j = i
    salida += funcion[j:]
    return salida

def limpiar_blancos(funcion):
    salida = ""
    for i in range(0, len(funcion)):
        if funcion[i] != " ":
            salida += funcion[i]
    return salida

reemplazos = {
    'sen' : 'numpy.sin',
    'cos' : 'numpy.cos',
    'exp': 'numpy.exp',
    'sqrt': 'numpy.sqrt',
    '^': '**',
}

def string2func(string):
    ''' evalua el string y retorna la funcion de x '''
    for antiguo, nuevo in reemplazos.items():
        string = string.replace(antiguo, nuevo)
    i = 0
    while i < len(string):
        inicio = i - 1
        if inicio < 0:
            inicio = 0 
        if string[i] == 'x' and string[inicio] >= '0' and string[inicio] <= '9':
            string = string[0:i] + '*' + string[i:]
            i += 1
        i += 1
    def func(x):
        return eval(string)
    
    return func
def test():
    print(limpiar_blancos("2x + 5x^2 - 20"))
    print(derivar("2x^5"))
    print(derivar('2^5'))
    print(derivar("3x^3+2x+5"))
    print(derivar("5x^4-2x^3+6x^2+x+1"))
    print(derivar("9x^5+12x^3+10x"))
    print(derivar("6x^3+2x^2-6x"))
    print(formatear_simple("6x^3+2x^2-6x"))
    print(derivar("x^(1/2)"))
    print(derivar("x^(1/3)+10x^3"))
    print(derivar("1/3x"))

def main():
    while True:
        funcion = input("Ingrese f(x)= ")
        if len(funcion) == 0:
            break
        derivada = derivar(funcion)
        print ("derivada f`(x)= ", derivada)
            
main()
