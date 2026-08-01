# Proyecto Taylor 📊📈

¡Una aplicación web interactiva desarrollada en **C#** utilizando **Blazor** para el cálculo, aproximación y visualización de funciones mediante Series de Taylor! 

---

## 🛠️ Tecnologías Utilizadas

* **.NET / C#** (Lógica de negocio, modelos y servicios de cálculo matemático)
* **Blazor** (Framework web interactivo con componentes Razor)
* **HTML5 & CSS3** (Diseño de la interfaz de usuario y estilos visuales)
* **JavaScript** (Funciones complementarias de soporte en el cliente)

---

## ⚙️ Características Principales

* **Cálculo de Series de Taylor:** Motor especializado para procesar aproximaciones matemáticas de alta precisión.
* **Arquitectura Modular (Servicios y Modelos):** Separación limpia entre la lógica de cálculo (`TaylorCaculoService`), la manipulación de vistas (`TaylorHtmlService`) y las entidades (`TaylorModel`).
* **Interfaz Interactiva (Blazor):** Páginas dinámicas desarrolladas con componentes Razor (`Taylor.razor`, `Creditos.razor`) para una experiencia fluida.

---

## 📁 Estructura del Proyecto

```text
ProyectoTaylor/
├── Components/
│   ├── Layout/          # Estructuras y plantillas de diseño web
│   └── Pages/
│       └── TaylorPages/
│           ├── Taylor.razor    # Vista principal de cálculo y aproximaciones
│           └── Creditos.razor  # Sección de créditos e información del proyecto
├── Models/
│   └── TaylorModel.cs       # Entidades y datos del modelo matemático
├── Services/
│   ├── TaylorCaculoService.cs   # Lógica matemática de las Series de Taylor
│   └── TaylorHtmlService.cs     # Servicios de renderizado y soporte HTML
└── wwwroot/                 # Archivos estáticos (CSS, imágenes, scripts)
