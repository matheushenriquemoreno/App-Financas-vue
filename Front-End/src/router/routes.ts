import { RouteRecordRaw } from 'vue-router';
import LoginPage from 'src/pages/LoginUserPage.vue';


import DespesaView from 'src/views/PageIndex/DespesaView.vue';
import RecebimentosView from 'src/views/PageIndex/RecebimentosView.vue';
import AcompanhamentoView from 'src/views/PageIndex/AcompanhamentoView.vue';


const routes: RouteRecordRaw[] = [
  {
    path: '/login',
    component: LoginPage,
  },
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      {
        path: '', name: 'Home', component: () => import('pages/IndexPage.vue'),
        children: [
          {
            path: '',
            name: 'recebimento',
            component: RecebimentosView
          },
          {
            path: 'Despesa',
            component: DespesaView
          },
          {
            path: 'Acompanhamento',
            component: AcompanhamentoView
          },
        ]
      }],
  },


  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue'),
  },
];

export default routes;
