
; Temat: Rozmazywanie obrazu metod� u�redniaj�c� (box blur)
; Opis: Algorytm przechodzi przez ca�� tablice bajt�w obrazu �r�d�owego i wpisuje do nowej tablicy �redni� warto�� pikseli wok� rozpatrywanego piksela.
; Autor: Jan Kwa�niak, Informatyka, rok 3, sem. 5, gr. 5, data: 22.12.2022
; Wersja: 0.9

.const 
jeden real4 1.0					;sta�a = 1, przyda si� to inkrementacji je�li warto�� jest w xmm
trzy real4 3.0					;sta�a = 3, przyda si� to inkrementacji je�li warto�� jest w xmm

.data

i qword ?						;licznik pierwszej p�tli
j qword ?						;licznik drugiej p�tli
j_end qword ?					;indeks koncowy drugiej p�tli

k sqword ?						;licznik trzeciej p�tli
l sqword ?						;licznik czwartej p�tli (w niej wykonywane jest sumowanie)
l_end sqword ?					;indeks ko�cowy czwartej p�tli

r sqword ?						;promie� s�siedztwa r
Tr sqword ?						;3*r
n qword ?						;wsp�czynnik, przez kt�ry dzielone s� sumy kolor�w

.code
BlurAsm proc 

mov rsi, rcx					;skopiowanie adresu tablicy obrazu wej�ciowego do rejestru rsi
mov rdi, rdx					;skopiowanie adresu tablicy obrazu wyj�ciowego do rejestru rdi

pxor xmm0, xmm0
pxor xmm1, xmm1
pxor xmm2, xmm2
pxor xmm3, xmm3
pxor xmm4, xmm4
pxor xmm5, xmm5
pxor xmm6, xmm6
pxor xmm7, xmm7
pxor xmm8, xmm8
pxor xmm9, xmm9
pxor xmm10, xmm10

CVTSS2SI r9, xmm0				;konwersja pierwszej warto�ci rejestru xmm0 do inta i skopiowanie jej do rejestru r9 (szeroko�� obrazu * 3)

shufps xmm0, xmm0, 225			;zamiana kolejno�ci pierwszej i drugiej warto�ci w xmm0 
CVTSS2SI r13, xmm0				;konwersja pierwszej warto�ci rejestru xmm0 do inta i skopiowanie jej do rejestru r13 (wysoko�� startowa)

shufps xmm0, xmm0, 198			;zamiana pierwszej warto�ci xmm0 z trzeci� 
CVTSS2SI r10, xmm0				;konwersja pierwszej warto�ci rejestru xmm0 do inta i skopiowanie jej do rejestru r11 (wysoko�� ko�cowa)

shufps xmm0, xmm0, 39			;zamiana pierwszej warto�ci xmm0 z czwart�
CVTSS2SI r8, xmm0				;konwersja pierwszej warto�ci rejestru xmm0 do inta i skopiowanie jej do rejestru r8 (promie� s�siedztwa)

mov r, r8						;przypisanie zmiennej r (promie� s�siedztwa) warto�ci rejestru r8
mov Tr, r8						;przypisanie zmiennej Tr (promie� s�siedztwa) warto�ci rejestru r8

mov rax, Tr						;skopiowanie Tr do akumulatora, w celu wykonania mno�enia
mov rbx, 3
mul rbx							;przemno�enie promienia s�siedztwa przez 3 (w celu iteracji co jeden pixel)
mov Tr, rax						;inicjalizacja licznika pierwszej, wewn�trznej p�tli 
mov j,	rax						;inicjalizacja licznika drugiej p�tli (zaczyna si� od 3*promie�)
mov l_end, rax					;przypisanie indeksu koncowego licznika l (licznik ostatniej p�tli iteruj�cego od -3r do 3r)
mov j_end, r9					;przypisanie do j_end szeroko�ci
sub j_end, rax					;odj�cie od szeroko�ci 3*r i przypisanie tej warto�ci do j_end (indeks ko�cowy drugiej p�tli)
mov r12, j_end					;skopiowanie zmiennej j_end do r12

mov rax, r						;skopiowanie do akumulatora warto�ci promienia 
sub rax, r						;odj�cie od promienia jego warto�ci w celu wyzerowania akumulatora
sub rax, r						;jeszcze jedno odj�cie w celu uzyskania -r

mov k, rax						;k = -r, gdzie k jest licznikiem trzeciej p�tli (od -r do r)
CVTSI2SS xmm1, k
CVTSI2SS xmm2, r8				;r jest tero w xmm2

mov rax, k						;skopiowanie do akumulatora pocz�tkowej warto�ci k = -r
mov rbx, 3						;skopiowanie do rbx 3, �eby przemno�yc k razy 3
imul rbx						;mno�enie ze znakiem
mov l, rax						;poczatkowa warto�� l = -3*r

CVTSI2SS xmm3, l				;skopiowanie warto�ci licznika czwartej p�tli (l) do rejestru xmm3
CVTSI2SS xmm4, Tr				;skopiowanie warto�ci 3*r do xmm4 (licznik ostatniej p�tli b�dzie por�wnywany do warto�ci tego rejestru)

mov r12, Tr						;skopiowanie do r12 Tr = 3*r (w celu por�wnywania w p�tli trzciej, k)

;obliczanie wsp�czynnika, przez kt�ry dzielona s� p�niej sumy dla poszczeg�lnych kolor�w
;n = (2*r+1)^2

mov rax, r						;skopiowanie r do akumulatora
shl rax, 1						;przemno�enie przez 2
inc rax							;zinkrementowanie wyniku 2*r
mov rbx, rax					;przeniesienie 2*r + 1 do rbx
mul rax							; (2*r+1)*(2*r+1)
mov n, rax						;n = (2*r+1)^2

CVTSI2SS xmm10, n				;skopiowanie do xmm10 warto�ci wsp�czynnika n

l1:
	cmp r13, r10						;por�wnanie aktualnej warto�ci licznika pierwszej p�tli (i) do wysoko�ci ko�cowej
	jge l1_koniec						;je�eli i>=wysoko�ci ko�cowej to zako�cz p�tle
	
	mov r11, r12						;je�eli i<wysoko�ci ko�cowej to przywr�c pocz�tkowe j, j = 3*r

	l2:
		cmp r11, j_end					;por�wnanie aktualnej warto�ci licznika p�tli drugiej do j_end = width - 3*r
		jge l2_koniec					;jezeli j==width-3*r to koniec p�tli

		;wyzerowanie rejestr�w, kt�re przechowuj� sumy kolejnych kolor�w (rgb)
		pxor xmm7, xmm7					;zerowanie rejestru xmm7, xmm7 przechowuje warto�� sumy koloru red
		pxor xmm8, xmm8					;zerowanie rejestru xmm8, xmm8 przechowuje warto�� sumy koloru green
		pxor xmm9, xmm9					;zerowanie rejestru xmm9, xmm9 przechowuje warto�� sumy koloru blue

		CVTSI2SS xmm1, k						;resetowanie k, k=-r

		l3:
			comiss xmm1, xmm2					;por�wnanie aktualnej warto�ci licznika p�tli (k) trzeciej do r
			ja l3_koniec						;je�eli wi�ksze, to zako�cz p�tle

			CVTSI2SS xmm3, l					;resetowanie l, l = -3r

			l4:
				comiss xmm3, xmm4				;por�wnanie licznika czwartej p�tli z 3*r
				ja l4_koniec					;je�eli wi�kszy lub r�wny, skocz do etykiety koncz�cej p�tle
		
				;generowanie (i+k)*width+j+l
				CVTSI2SS xmm5, r13				;kopiowanie i do xmm5
				addss xmm5, xmm1				;i+k, k jest w xmm1
				CVTSI2SS xmm6, r9				;kopiowanie width do xmm6
				mulss xmm5, xmm6				;(i+k)*width 
				CVTSI2SS xmm6, r11				;kopiowanie j do xmm6
				addss xmm5, xmm6				;(i+k)*width + j
				addss xmm5, xmm3				;(i+k)*width + j + l ->l jest w xmm3

				CVTSS2SI rax, xmm5				;przeniesienie xmm5, czyli offesetu (i+k)*width + j + l do rax (rozkaz ten konwertuje floata z xmma do inta)

				;zwi�kszanie sumy red
				xor rbx, rbx					;wyzerowanie rejestru rbx, �eby wpisa� do niego warto�� koloru
				mov bl, [rsi+rax+2]				;zachowanie w bl warto�ci sourceData[(i+k)*width +j+l+2]
				CVTSI2SS xmm5, rbx				;przeniesienie do xmm5 sourceData[(i+k)*width +j+l+2]
				addss xmm7, xmm5				;sumR+=sourceData[(i+k)*width +j+l]

				;zwi�kszanie sumy green
				mov bl, [rsi + rax + 1]			;skopiowanie do bl warto�ci green z aktualnie rozpatrywanego pixela
				CVTSI2SS xmm5, rbx				;skopiowanie jego warto�ci do xmm5
				addss xmm8, xmm5				;sumG+=sourceData[(i+k)*width +j+l + 1]

				;zwi�kszanie sumy blue
				mov bl, [rsi + rax]				;skopiowanie do bl warto�ci blue z aktualnie rozpatrywanego pixela
				CVTSI2SS xmm5, rbx				;skopiowanie jego warto�ci do xmm5
				addss xmm9, xmm5				;sumB+=sourceData[(i+k)*width +j+l]

				addss xmm3, trzy				;zwi�kszenie licznika czwartej p�tli (l) o 3
			jmp l4

			l4_koniec:
				addss xmm1, jeden				;zwi�kszenie licznika trzeciej p�tli (k) o 1
				jmp l3

		l3_koniec:
			
			;wyliczanie i*width+j, w celu wpisywania do tablicy wyjsciowej uzyskanych warto�ci �rednich kolor�w 
			xor rax, rax

			mov rax, r13				;skopiowanie do akumulatora warto�ci licznika pierwszej p�tli (i)
			mul r9						;przemno�enie i przez width
			add rax, r11				;dodanie do i*width warto�ci zmiennej j
			xor rbx, rbx				;zerowanie rejestru rbx (mog� by� w nim warto�ci z poprzednich operacji, kt�re teraz ju� nie sa potrzebne)

			;red
			divss xmm7, xmm10			;w rejestrze xmm10 jest warto�� wsp�czynnika n, dzielimy sum� red przez jego warto��
			CVTTSS2SI rbx, xmm7			;skopiowanie do rbx warto�ci uzyskanej w poprzednim dzieleniu, rozkaz CVTTSS2SI konwertuje flota z xmm na int ucinaj�c liczby po przecinku
			mov [rdi+rax + 2], bl		;wpisanie do zwracanej tablicy znaku spod offset + i*width+j

			;green
			xor rbx, rbx
			divss xmm8, xmm10			;w rejestrze xmm10 jest warto�� wsp�czynnika n, dzielimy sum� green przez jego warto��
			CVTTSS2SI rbx, xmm8			;skopiowanie do rbx warto�ci uzyskanej w poprzednim dzieleniu
			mov [rdi+rax+1], bl			;wpisanie do zwracanej tablicy znaku spod offset + i*width+j+1
			
			;blue
			xor rbx, rbx
			divss xmm9, xmm10			;w rejestrze xmm10 jest warto�� wsp�czynnika n, dzielimy sum� blue przez jego warto��
			CVTTSS2SI rbx, xmm9			;skopiowanie do rbx warto�ci uzyskanej w poprzednim dzieleniu
			mov [rdi+rax], bl			;wpisanie do zwracanej tablicy znaku z offset + i*width+j+2

			add r11, 3					;j+=3
			jmp l2

	l2_koniec:
		inc r13							;i++
		jmp l1

l1_koniec:
	mov rax, rdi	
	ret

BlurAsm endp
end