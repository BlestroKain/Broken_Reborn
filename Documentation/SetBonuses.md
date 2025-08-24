# Bonificaciones de Conjuntos

Los porcentajes otorgados por múltiples conjuntos se **acumulan de forma aditiva**. 
Por ejemplo, si dos conjuntos distintos aportan +10 % al ataque cada uno, el resultado final será +20 %.
No se aplican de manera multiplicativa.

Las bonificaciones se aplican mediante `ApplySetBonuses(Player p)`,
que combina todos los conjuntos activos en orden alfabético,
asegurando resultados consistentes sin importar el orden en que se equipen los objetos.
