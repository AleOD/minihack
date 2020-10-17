'''Python Modules'''
import csv
import firebase_admin

from firebase_admin import credentials, firestore

cred = credentials.Certificate("minihack-firebase.json")
app = firebase_admin.initialize_app(cred)

db = firestore.client()

file_path = './astroAsteroids/Assets/Report/report.csv'


def extract_data():
    with open(file_path) as csv_file: 
        csv_reader = csv.DictReader(csv_file) # Parses the contents of Training.csv as dictionaries

        fila = 0
        for row in csv_reader:
            data = {
                u'usuario': row['usuario'],
                u'boton_correcto': row['boton correcto'],
                u'tiempo_de_interaccion': row['tiempo de interaccion'],
                u'mini_juego': row['mini juego'],
                u'numero_de_interaccion': row['numero de interaccion'],
                u'color_presionado': row['color presionado'],
                u'dificultad': row['dificultad'],
                u'fecha': row['fecha']
            }
            datos_jugador = db.collection(u'Astro').document(u'datos_jugador'+ str(fila))
            datos_jugador.set(data, merge=True)
            fila += 1
                

''' Starts execution of the program '''
if __name__ == "__main__":         
    extract_data()