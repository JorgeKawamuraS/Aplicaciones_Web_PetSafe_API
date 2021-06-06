

@mytag
Scenario: Calificar una veterinaria correctamente
	Given el dueño se encuentra en el perfil de la veterinaria
	And escriba un comentario con su respectiva cantidad de estrellas
	When le de click en comentar
	Then el comentario se habrá publicado correctamente