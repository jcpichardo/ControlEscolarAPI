-- Crear los esquemas
CREATE SCHEMA IF NOT EXISTS seguridad;
CREATE SCHEMA IF NOT EXISTS escolar;

-- Crear tabla personas en el esquema seguridada
CREATE TABLE seguridad.personas (
    id SERIAL PRIMARY KEY,  -- En PostgreSQL se usa SERIAL en lugar de IDENTITY
    nombre_completo VARCHAR(255) NOT NULL,
    correo VARCHAR(100) NOT NULL,  -- Correo electrónico de la persona
    telefono VARCHAR(15) NOT NULL,  -- Número de teléfono de la persona
    curp VARCHAR(18) NOT NULL, -- CURP  de la persona
    fecha_nacimiento DATE NULL,  -- Fecha de nacimiento de la persona
    estatus BOOLEAN NOT NULL DEFAULT TRUE  -- En PostgreSQL se usa BOOLEAN en lugar de BIT
);

-- Crear tabla estudiantes en el esquema escolar
CREATE TABLE escolar.estudiantes (
    id SERIAL PRIMARY KEY,  -- En PostgreSQL se usa SERIAL en lugar de IDENTITY
    id_persona INTEGER NOT NULL,  -- Relación con la tabla personas
    matricula VARCHAR(20) NOT NULL UNIQUE,
    semestre VARCHAR(20) NULL,  -- Semestre o grado en el que está inscrito
    fecha_alta DATE NOT NULL DEFAULT CURRENT_DATE,  -- En PostgreSQL se usa CURRENT_DATE en lugar de GETDATE()
    fecha_baja DATE NULL,  -- Fecha en la que el estudiante se dio de baja (NULL si sigue activo)
    estatus SMALLINT NOT NULL DEFAULT 1,  -- Estado del estudiante: 0 = Baja, 1 = Activo, 2 = Baja Temporal
    CONSTRAINT chk_estudiantes_estatus CHECK (estatus IN (0, 1, 2)),
    CONSTRAINT fk_estudiantes_personas FOREIGN KEY (id_persona) REFERENCES seguridad.personas(id)
);

-- Crear índices para mejorar el rendimiento
CREATE INDEX idx_estudiantes_id_persona ON escolar.estudiantes(id_persona);
CREATE INDEX idx_estudiantes_matricula ON escolar.estudiantes(matricula);
CREATE INDEX idx_personas_nombre ON seguridad.personas(nombre_completo);

-- Comentarios en las tablas y columnas
COMMENT ON TABLE seguridad.personas IS 'Tabla que almacena información básica de todas las personas';
COMMENT ON COLUMN seguridad.personas.id IS 'Identificador único de la persona';
COMMENT ON COLUMN seguridad.personas.nombre_completo IS 'Nombre completo de la persona';
COMMENT ON COLUMN seguridad.personas.correo IS 'Correo electrónico de contacto';
COMMENT ON COLUMN seguridad.personas.telefono IS 'Número telefónico de contacto';
COMMENT ON COLUMN seguridad.personas.fecha_nacimiento IS 'Fecha de nacimiento de la persona';
COMMENT ON COLUMN seguridad.personas.estatus IS 'Estado de la persona: TRUE=Activo, FALSE=Inactivo';

COMMENT ON TABLE escolar.estudiantes IS 'Tabla que almacena información de los estudiantes';
COMMENT ON COLUMN escolar.estudiantes.id IS 'Identificador único del estudiante';
COMMENT ON COLUMN escolar.estudiantes.id_persona IS 'Referencia al ID en la tabla personas';
COMMENT ON COLUMN escolar.estudiantes.matricula IS 'Código de matrícula único del estudiante';
COMMENT ON COLUMN escolar.estudiantes.semestre IS 'Semestre o grado actual del estudiante';
COMMENT ON COLUMN escolar.estudiantes.fecha_alta IS 'Fecha de registro del estudiante en el sistema';
COMMENT ON COLUMN escolar.estudiantes.fecha_baja IS 'Fecha en que el estudiante causó baja, NULL si sigue activo';
COMMENT ON COLUMN escolar.estudiantes.estatus IS 'Estado del estudiante: 0=Baja, 1=Activo, 2=Baja Temporal';
