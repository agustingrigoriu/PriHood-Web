import { Injectable } from '@angular/core';
import { ApiRequestService } from '../../services/api.request.service';
import { RootDashboard, AdminDashboard } from './home.model';

@Injectable()
export class DashboardService {
  constructor(protected request: ApiRequestService) { }

  getRootDashboard() {
    return this.request.get<any>(`dashboard/root`);
  }

  getAdminDashboard() {
    return this.request.get<any>(`dashboard/administrador`);
  }

}