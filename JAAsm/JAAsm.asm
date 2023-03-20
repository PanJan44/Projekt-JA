
; Temat: Rozmazywanie obrazu metod¹ uœredniaj¹c¹ (box blur)
; Opis: Algorytm przechodzi przez ca³¹ tablice bajtów obrazu Ÿród³owego i wpisuje do nowej tablicy œredni¹ wartoœæ pikseli wokó³ rozpatrywanego piksela.
; Autor: Jan Kwaœniak, Informatyka, rok 3, sem. 5, gr. 5, data: 22.12.2022
; Wersja: 0.9

.const 
jeden real4 1.0					;sta³a = 1, przyda siê to inkrementacji jeœli wartoœæ jest w xmm
trzy real4 3.0					;sta³a = 3, przyda siê to inkrementacji jeœli wartoœæ jest w xmm

.data

i qword ?						;licznik pierwszej pêtli
j qword ?						;licznik drugiej pêtli
j_end qword ?					;indeks koncowy drugiej pêtli

k sqword ?						;licznik trzeciej pêtli
l sqword ?						;licznik czwartej pêtli (w niej wykonywane jest sumowanie)
l_end sqword ?					;indeks koñcowy czwartej pêtli

r sqword ?						;promieñ s¹siedztwa r
Tr sqword ?						;3*r
n qword ?						;wspó³czynnik, przez który dzielone s¹ sumy kolorów

.code
BlurAsm proc 

mov rsi, rcx					;skopiowanie adresu tablicy obrazu wejœciowego do rejestru rsi
mov rdi, rdx					;skopiowanie adresu tablicy obrazu wyjœciowego do rejestru rdi

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

CVTSS2SI r9, xmm0				;konwersja pierwszej wartoœci rejestru xmm0 do inta i skopiowanie jej do rejestru r9 (szerokoœæ obrazu * 3)

shufps xmm0, xmm0, 225			;zamiana kolejnoœci pierwszej i drugiej wartoœci w xmm0 
CVTSS2SI r13, xmm0				;konwersja pierwszej wartoœci rejestru xmm0 do inta i skopiowanie jej do rejestru r13 (wysokoœæ startowa)

shufps xmm0, xmm0, 198			;zamiana pierwszej wartoœci xmm0 z trzeci¹ 
CVTSS2SI r10, xmm0				;konwersja pierwszej wartoœci rejestru xmm0 do inta i skopiowanie jej do rejestru r11 (wysokoœæ koñcowa)

shufps xmm0, xmm0, 39			;zamiana pierwszej wartoœci xmm0 z czwart¹
CVTSS2SI r8, xmm0				;konwersja pierwszej wartoœci rejestru xmm0 do inta i skopiowanie jej do rejestru r8 (promieñ s¹siedztwa)

mov r, r8						;przypisanie zmiennej r (promieñ s¹siedztwa) wartoœci rejestru r8
mov Tr, r8						;przypisanie zmiennej Tr (promieñ s¹siedztwa) wartoœci rejestru r8

mov rax, Tr						;skopiowanie Tr do akumulatora, w celu wykonania mno¿enia
mov rbx, 3
mul rbx							;przemno¿enie promienia s¹siedztwa przez 3 (w celu iteracji co jeden pixel)
mov Tr, rax						;inicjalizacja licznika pierwszej, wewnêtrznej pêtli 
mov j,	rax						;inicjalizacja licznika drugiej pêtli (zaczyna siê od 3*promieñ)
mov l_end, rax					;przypisanie indeksu koncowego licznika l (licznik ostatniej pêtli iteruj¹cego od -3r do 3r)
mov j_end, r9					;przypisanie do j_end szerokoœci
sub j_end, rax					;odjêcie od szerokoœci 3*r i przypisanie tej wartoœci do j_end (indeks koñcowy drugiej pêtli)
mov r12, j_end					;skopiowanie zmiennej j_end do r12

mov rax, r						;skopiowanie do akumulatora wartoœci promienia 
sub rax, r						;odjêcie od promienia jego wartoœci w celu wyzerowania akumulatora
sub rax, r						;jeszcze jedno odjêcie w celu uzyskania -r

mov k, rax						;k = -r, gdzie k jest licznikiem trzeciej pêtli (od -r do r)
CVTSI2SS xmm1, k
CVTSI2SS xmm2, r8				;r jest tero w xmm2

mov rax, k						;skopiowanie do akumulatora pocz¹tkowej wartoœci k = -r
mov rbx, 3						;skopiowanie do rbx 3, ¿eby przemno¿yc k razy 3
imul rbx						;mno¿enie ze znakiem
mov l, rax						;poczatkowa wartoœæ l = -3*r

CVTSI2SS xmm3, l				;skopiowanie wartoœci licznika czwartej pêtli (l) do rejestru xmm3
CVTSI2SS xmm4, Tr				;skopiowanie wartoœci 3*r do xmm4 (licznik ostatniej pêtli bêdzie porównywany do wartoœci tego rejestru)

mov r12, Tr						;skopiowanie do r12 Tr = 3*r (w celu porównywania w pêtli trzciej, k)

;obliczanie wspó³czynnika, przez który dzielona s¹ póŸniej sumy dla poszczególnych kolorów
;n = (2*r+1)^2

mov rax, r						;skopiowanie r do akumulatora
shl rax, 1						;przemno¿enie przez 2
inc rax							;zinkrementowanie wyniku 2*r
mov rbx, rax					;przeniesienie 2*r + 1 do rbx
mul rax							; (2*r+1)*(2*r+1)
mov n, rax						;n = (2*r+1)^2

CVTSI2SS xmm10, n				;skopiowanie do xmm10 wartoœci wspó³czynnika n

l1:
	cmp r13, r10						;porównanie aktualnej wartoœci licznika pierwszej pêtli (i) do wysokoœci koñcowej
	jge l1_koniec						;je¿eli i>=wysokoœci koñcowej to zakoñcz pêtle
	
	mov r11, r12						;je¿eli i<wysokoœci koñcowej to przywróc pocz¹tkowe j, j = 3*r

	l2:
		cmp r11, j_end					;porównanie aktualnej wartoœci licznika pêtli drugiej do j_end = width - 3*r
		jge l2_koniec					;jezeli j==width-3*r to koniec pêtli

		;wyzerowanie rejestrów, które przechowuj¹ sumy kolejnych kolorów (rgb)
		pxor xmm7, xmm7					;zerowanie rejestru xmm7, xmm7 przechowuje wartoœæ sumy koloru red
		pxor xmm8, xmm8					;zerowanie rejestru xmm8, xmm8 przechowuje wartoœæ sumy koloru green
		pxor xmm9, xmm9					;zerowanie rejestru xmm9, xmm9 przechowuje wartoœæ sumy koloru blue

		CVTSI2SS xmm1, k						;resetowanie k, k=-r

		l3:
			comiss xmm1, xmm2					;porównanie aktualnej wartoœci licznika pêtli (k) trzeciej do r
			ja l3_koniec						;je¿eli wiêksze, to zakoñcz pêtle

			CVTSI2SS xmm3, l					;resetowanie l, l = -3r

			l4:
				comiss xmm3, xmm4				;porównanie licznika czwartej pêtli z 3*r
				ja l4_koniec					;je¿eli wiêkszy lub równy, skocz do etykiety koncz¹cej pêtle
		
				;generowanie (i+k)*width+j+l
				CVTSI2SS xmm5, r13				;kopiowanie i do xmm5
				addss xmm5, xmm1				;i+k, k jest w xmm1
				CVTSI2SS xmm6, r9				;kopiowanie width do xmm6
				mulss xmm5, xmm6				;(i+k)*width 
				CVTSI2SS xmm6, r11				;kopiowanie j do xmm6
				addss xmm5, xmm6				;(i+k)*width + j
				addss xmm5, xmm3				;(i+k)*width + j + l ->l jest w xmm3

				CVTSS2SI rax, xmm5				;przeniesienie xmm5, czyli offesetu (i+k)*width + j + l do rax (rozkaz ten konwertuje floata z xmma do inta)

				;zwiêkszanie sumy red
				xor rbx, rbx					;wyzerowanie rejestru rbx, ¿eby wpisaæ do niego wartoœæ koloru
				mov bl, [rsi+rax+2]				;zachowanie w bl wartoœci sourceData[(i+k)*width +j+l+2]
				CVTSI2SS xmm5, rbx				;przeniesienie do xmm5 sourceData[(i+k)*width +j+l+2]
				addss xmm7, xmm5				;sumR+=sourceData[(i+k)*width +j+l]

				;zwiêkszanie sumy green
				mov bl, [rsi + rax + 1]			;skopiowanie do bl wartoœci green z aktualnie rozpatrywanego pixela
				CVTSI2SS xmm5, rbx				;skopiowanie jego wartoœci do xmm5
				addss xmm8, xmm5				;sumG+=sourceData[(i+k)*width +j+l + 1]

				;zwiêkszanie sumy blue
				mov bl, [rsi + rax]				;skopiowanie do bl wartoœci blue z aktualnie rozpatrywanego pixela
				CVTSI2SS xmm5, rbx				;skopiowanie jego wartoœci do xmm5
				addss xmm9, xmm5				;sumB+=sourceData[(i+k)*width +j+l]

				addss xmm3, trzy				;zwiêkszenie licznika czwartej pêtli (l) o 3
			jmp l4

			l4_koniec:
				addss xmm1, jeden				;zwiêkszenie licznika trzeciej pêtli (k) o 1
				jmp l3

		l3_koniec:
			
			;wyliczanie i*width+j, w celu wpisywania do tablicy wyjsciowej uzyskanych wartoœci œrednich kolorów 
			xor rax, rax

			mov rax, r13				;skopiowanie do akumulatora wartoœci licznika pierwszej pêtli (i)
			mul r9						;przemno¿enie i przez width
			add rax, r11				;dodanie do i*width wartoœci zmiennej j
			xor rbx, rbx				;zerowanie rejestru rbx (mog¹ byæ w nim wartoœci z poprzednich operacji, które teraz ju¿ nie sa potrzebne)

			;red
			divss xmm7, xmm10			;w rejestrze xmm10 jest wartoœæ wspó³czynnika n, dzielimy sumê red przez jego wartoœæ
			CVTTSS2SI rbx, xmm7			;skopiowanie do rbx wartoœci uzyskanej w poprzednim dzieleniu, rozkaz CVTTSS2SI konwertuje flota z xmm na int ucinaj¹c liczby po przecinku
			mov [rdi+rax + 2], bl		;wpisanie do zwracanej tablicy znaku spod offset + i*width+j

			;green
			xor rbx, rbx
			divss xmm8, xmm10			;w rejestrze xmm10 jest wartoœæ wspó³czynnika n, dzielimy sumê green przez jego wartoœæ
			CVTTSS2SI rbx, xmm8			;skopiowanie do rbx wartoœci uzyskanej w poprzednim dzieleniu
			mov [rdi+rax+1], bl			;wpisanie do zwracanej tablicy znaku spod offset + i*width+j+1
			
			;blue
			xor rbx, rbx
			divss xmm9, xmm10			;w rejestrze xmm10 jest wartoœæ wspó³czynnika n, dzielimy sumê blue przez jego wartoœæ
			CVTTSS2SI rbx, xmm9			;skopiowanie do rbx wartoœci uzyskanej w poprzednim dzieleniu
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