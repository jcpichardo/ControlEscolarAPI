CREATE OR REPLACE FUNCTION escolar.udf_obtener_estudiantes(
    p_solo_activos BOOLEAN DEFAULT TRUE,
    p_tipo_fecha INTEGER DEFAULT 0,
    p_fecha_inicio DATE DEFAULT NULL,
    p_fecha_fin DATE DEFAULT NULL
)
RETURNS TABLE (
    id INTEGER,
    matricula VARCHAR,
    semestre VARCHAR,
    fecha_alta TIMESTAMP,
    fecha_baja TIMESTAMP,
    estatus INTEGER,
    descestatus_estudiante VARCHAR,
    id_persona INTEGER,
    nombre_completo VARCHAR,
    correo VARCHAR,
    telefono VARCHAR,
    fecha_nacimiento DATE,
    curp VARCHAR,
    estatus_persona BOOLEAN
) AS $$
BEGIN
    RETURN QUERY
    SELECT 
        e.id, e.matricula, e.semestre, e.fecha_alta, e.fecha_baja, e.estatus,
        CASE 
            WHEN e.estatus = 0 THEN 'Baja'
            WHEN e.estatus = 1 THEN 'Activo'
            WHEN e.estatus = 2 THEN 'Baja temporal'
            ELSE 'Desconocido'
        END AS descestatus_estudiante,
        e.id_persona, p.nombre_completo, p.correo, p.telefono, p.fecha_nacimiento, p.curp, p.estatus as estatus_persona
    FROM 
        escolar.estudiantes e
    INNER JOIN 
        seguridad.personas p ON e.id_persona = p.id
    WHERE 
        1=1
        AND (
            (NOT p_solo_activos) -- Si queremos todos los registros
            OR 
            (p_solo_activos AND e.estatus = 1) -- Si solo queremos activos
        )
        AND (
            (p_tipo_fecha = 1 AND p_fecha_inicio IS NOT NULL AND p_fecha_fin IS NOT NULL 
                AND p.fecha_nacimiento BETWEEN p_fecha_inicio AND p_fecha_fin)
            OR (p_tipo_fecha = 2 AND p_fecha_inicio IS NOT NULL AND p_fecha_fin IS NOT NULL 
                AND e.fecha_alta BETWEEN p_fecha_inicio AND p_fecha_fin)
            OR (p_tipo_fecha = 3 AND p_fecha_inicio IS NOT NULL AND p_fecha_fin IS NOT NULL 
                AND e.fecha_baja BETWEEN p_fecha_inicio AND p_fecha_fin)
            OR (p_tipo_fecha NOT IN (1, 2, 3)) -- Si no es ninguno de los anteriores, no se filtra por fecha
        )
    ORDER BY 
        e.id;
END;
$$ LANGUAGE plpgsql;