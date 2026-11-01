CREATE DATABASE IF NOT EXISTS tienda_db
  CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

USE tienda_db;

CREATE TABLE IF NOT EXISTS productos (
    id             INT AUTO_INCREMENT PRIMARY KEY,
    nombre         VARCHAR(100)  NOT NULL,
    descripcion    VARCHAR(255)  NULL,
    categoria      VARCHAR(50)   NOT NULL DEFAULT 'General',
    precio         DECIMAL(10,2) NOT NULL,
    stock          INT           NOT NULL DEFAULT 0,
    fecha_registro DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Datos de ejemplo
INSERT INTO productos (nombre, descripcion, categoria, precio, stock) VALUES
('Laptop Lenovo 15"', 'Intel Core i5, 16GB RAM, 512GB SSD', 'Computo', 14999.00, 10),
('Mouse inalámbrico', 'Mouse óptico 2.4GHz', 'Accesorios', 249.50, 80),
('Teclado mecánico', 'Switch rojo, retroiluminado', 'Accesorios', 899.00, 35),
('Monitor 24"', 'Full HD IPS 75Hz', 'Computo', 2799.00, 15);
