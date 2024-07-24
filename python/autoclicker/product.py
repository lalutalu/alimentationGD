# Product class
class Product:
    def __init__(self, name, prix, code39, weight, unit, quantity) -> None:
        self.name = name
        self.prix = prix
        self.code39 = code39
        self.quantity = quantity
        self.weight = weight
        self.unit = unit
