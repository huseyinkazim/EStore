import { Component, OnInit } from '@angular/core';
import { CouponService } from '../service/coupon.service';
import { CouponDto } from '../model/coupondto';

@Component({
  selector: 'app-coupon',
  templateUrl: './coupon.component.html',
  styleUrls: ['./coupon.component.css'],
})
export class CouponComponent implements OnInit {
  coupons: CouponDto[];
  selectedCoupon: CouponDto;
  editingCoupon: CouponDto;
  creatingCoupon: CouponDto = new CouponDto();
  errorMessage: string = '';
  successMessage: string = '';
  isOpenCreateForm: boolean = false;

  constructor(private couponService: CouponService) {}

  ngOnInit(): void {
    this.loadAllCoupons();
  }

  loadAllCoupons() {
    this.couponService.getAll().subscribe(
      (data) => {
        this.coupons = data.result;
      },
      (error) => {
        this.errorMessage = 'Failed to load coupons.';
      }
    );
  }

  selectCoupon(coupon: any) {
    this.selectedCoupon = coupon;
    this.editingCoupon = null; // İlgili kuponu düzenlemek için formu gizle
  }

  editCoupon(coupon: any) {
    this.selectedCoupon = null; // Seçili kuponu temizle
    this.editingCoupon = { ...coupon }; // Kuponu düzenlemek için formu göster
  }

  updateCoupon(coupon: any) {
    this.couponService.update(coupon).subscribe(
      (data) => {
        this.successMessage = 'Coupon updated successfully.';
        this.loadAllCoupons();
        this.editingCoupon = null; // Düzenleme formunu kapat
      },
      (error) => {
        this.errorMessage = 'Failed to update the coupon.';
      }
    );
  }

  createCoupon(coupon: any) {
  
    this.couponService.create(coupon).subscribe(
      (data) => {
        this.successMessage = 'Coupon created successfully.';
        this.loadAllCoupons();
        this.clearCreateForm();
      },
      (error) => {
        this.errorMessage = 'Failed to create a coupon.';
      }
    );
  }

  deleteCoupon(id: number) {
    this.couponService.delete(id).subscribe(
      () => {
        this.successMessage = 'Coupon deleted successfully.';
        this.loadAllCoupons();
      },
      (error) => {
        this.errorMessage = 'Failed to delete the coupon.';
      }
    );
  }
  openCreateFrom(){
    this.isOpenCreateForm=true;
  }
  cancelEdit() {
    this.editingCoupon = null; // Düzenlemeyi iptal et
  }
  cancelCreate() {
    this.creatingCoupon = null; // Oluşturmayı iptal et
  }
  clearCreateForm() {
    this.isOpenCreateForm=false;
    this.creatingCoupon = null; 
  }
}
