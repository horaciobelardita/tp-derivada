import sys

from PyQt4 import QtGui
from matplotlib.backends.backend_qt4agg import FigureCanvasQTAgg as FigureCanvas
from matplotlib.figure import Figure
from numpy import linspace
import matplotlib.pyplot as plt
from derivar import string2func, derivar


class ApplicationWindow(QtGui.QWidget):
    def __init__(self):
        QtGui.QWidget.__init__(self)


        self.resize(400, 400)

        self.setWindowTitle('Calculo de Derivada')

        self.figure = plt.figure()

        self.canvas = FigureCanvas(self.figure)
        
        self.funcion = QtGui.QLineEdit(self)

        self.derivada = QtGui.QLineEdit(self)
        self.derivada.setEnabled(False)

        # Just some button connected to `plot` method
        self.boton_graficar = QtGui.QPushButton('Graficar')
        self.boton_graficar.setEnabled(False)
        self.boton_graficar.clicked.connect(self.graficar)
        self.boton_derivar = QtGui.QPushButton("Derivar")
        self.boton_derivar.clicked.connect(self.calcular)

        # set the layout
        layout = QtGui.QVBoxLayout()
        layout.addWidget(self.canvas)
        grid = QtGui.QGridLayout()
        grid.addWidget(QtGui.QLabel("f(x)="), 0, 0)
        grid.addWidget(self.funcion, 0, 1)
        grid.addWidget(QtGui.QLabel("f'(x)="), 1, 0)
        grid.addWidget(self.derivada, 1, 1)
        layout.addLayout(grid)
        layout.addWidget(self.boton_derivar)
        layout.addWidget(self.boton_graficar)

        self.setLayout(layout)
    def graficar(self):

        plt.clf()

        # create an axis
        ax = self.figure.add_subplot(111)

        x = linspace(-15,15,100)
        f = string2func(self.fx)

        ax.plot(x, f(x), label='f(x)')

        x = linspace(-15,15,250)
        f = string2func(self.dfx)

        ax.plot(x, f(x), label="f'(x)")

        ax.legend(loc='upper right')


        self.canvas.draw()

    def calcular(self):
        self.fx = str(self.funcion.text())
        if len(self.fx) == 0:
            msgBox = QtGui.QMessageBox()
            msgBox.setText("Debe ingresar una funcion f(x)")
            msgBox.exec_()
            self.funcion.setFocus()
            return

        self.dfx = str(derivar(self.fx))
        self.derivada.setText(self.dfx)
        self.derivada.setEnabled(True)
        self.boton_graficar.setEnabled(True)


app = QtGui.QApplication(sys.argv)

window = ApplicationWindow()

window.show()

sys.exit(app.exec_())
