import { defineStore } from 'pinia'
import { reactive, ref } from 'vue'


interface Entity {
  id: number | string
}

interface GenericService<T extends Entity> {
  create(item: T): Promise<void>
  read(): Promise<T[]>
  update(id: number, data: Partial<T>): Promise<void>
  delete(id: number): Promise<void>
  getById(id: number): Promise<T>
}

export const useGenericStore = <T extends Entity>(service: GenericService<T>) => {
  return defineStore('genericStore', {
    state: () => ({
      items: [] as Array<T>,
    }),
    getters: {
      obterItems: async (state) => {
        if (state.items.length <= 0) {
          const items = await service.read()
          state.items = reactive<T[]>(items)
        }

        return state.items;
      },
      obterUsuarioPeloid: (state) => {
        return (userId: string | number) => state.items.find((user) => user.id === userId)
      },
    },
    actions: {
      async create(item: T): Promise<void> {
        await service.create(item)
        await this.atualizarDados()
      },
      async atualizarDados(): Promise<void> {
        const items = await service.read()
        this.items = reactive<T[]>(items)
      },
      async update(id: number, data: Partial<T>): Promise<void> {
        await service.update(id, data)
        await this.atualizarDados()
      },
      async delete(id: number): Promise<void> {
        await service.delete(id)
        await this.atualizarDados()
      },
      async obterUsuarioPeloId(id: number | string) {
        if (this.items.length <= 0) {
          await this.atualizarDados()
        }

        const result = this.items.find((item) => item.id === id);

        if (result === undefined) {
          return service.getById(parseInt(id.toString()));
        }

        return result;
      }

    }
  })
}
