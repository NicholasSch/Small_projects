#include <stdio.h>
#include <time.h>

typedef enum{
    PRONTO,
    EM_EXECUCAO,
    BLOQUEADO,
    TERMINADO
} EstadoProcesso;

//Estrutura p/ representar um processo
typedef struct{
    int id;
    int tempo_restante;
    EstadoProcesso estado;
}Processo;

void delay(int number_of_milli_seconds)
{
    clock_t start_time = clock();
 
    while (clock() < start_time + number_of_milli_seconds);
}

void roundRobin(Processo processos[], int num_processos){
    int quantum = 10;
    int processosincompletos = 0;
    int tempo_atual = 0;
    int tempo_entre_processos = 2;
    for (int i = 0; i < num_processos; i++) {
        if (processos[i].estado == PRONTO) 
        {
            tempo_atual+=tempo_entre_processos;
            printf("Tempo %d: Processo %d em execução\n", tempo_atual, processos[i].id);
            processos[i].estado = EM_EXECUCAO;
            if (processos[i].tempo_restante>quantum)
            {
            processosincompletos++;
            processos[i].tempo_restante -= quantum;
            processos[i].estado = PRONTO;
            tempo_atual += quantum;
            delay(quantum);
            printf("processo %d não completado no tempo %d\n",processos[i].id, tempo_atual);
            }
             else
            {
            processos[i].estado = TERMINADO;
            tempo_atual += processos[i].tempo_restante;
            delay(processos[i].tempo_restante);
            printf("processo %d completo no tempo %d\n",processos[i].id, tempo_atual);
            }
        }
        if (i == num_processos-1)
        {
            if (processosincompletos>=1)
            {
                i=-1;
                processosincompletos=0;
            }
        }
    }
}


int main(){
    Processo processos[] = {
        {1, 14, PRONTO},
        {2, 2, PRONTO},
        {3, 60, PRONTO},
        {4, 8, PRONTO}
    }; 
        int num_processos = sizeof(processos)/sizeof(processos[0]);
        roundRobin(processos, num_processos);
    return 0;
};