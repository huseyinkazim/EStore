import { Component, OnInit } from '@angular/core';
import { CouponService } from '../service/coupon.service';
import { CouponDto } from '../model/coupondto';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-coupon',
  templateUrl: './coupon.component.html',
  styleUrls: ['./coupon.component.css'],
})
export class CouponComponent implements OnInit {
  coupons: CouponDto[];
  selectedCoupon: CouponDto;
  editingCoupon: CouponDto = new CouponDto();
  creatingCoupon: CouponDto = new CouponDto();
  isOpenCreateForm: boolean = false;
  isOpenEditForm: boolean = false;
  isOpenSelectedCoupon: boolean = false;

  constructor(private couponService: CouponService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadAllCoupons();
  }

  loadAllCoupons() {
    this.couponService.getAll().subscribe(
      (data) => {
        this.coupons = data.result;
      },
      (error) => {
        console.log(error);
        this.toastr.error('Kupon yüklerken beklenmedik hata!', 'Yükleme Hatası');
      }
    );
  }

  selectCoupon(coupon: any) {
    this.clearAllForm();
    this.selectedCoupon = coupon;
    this.isOpenSelectedCoupon = true;
  }

  editCoupon(coupon: any) {
    this.clearAllForm();
    this.isOpenEditForm = true;
    this.editingCoupon = { ...coupon }; // Kuponu düzenlemek için formu göster
  }

  updateCoupon(coupon: any) {
    this.couponService.update(coupon).subscribe(
      (data) => {
        this.loadAllCoupons();
        this.clearEditingCuopon();
        this.toastr.success('Kupon başarılı güncellendi', 'Güncelleme Onay');
      },
      (error) => {
        console.log(error);
        this.toastr.error('Kupon güncellenirken beklenmedik hata!', 'Güncelleme Hatası');
      }
    );
  }

  createCoupon(coupon: any) {

    this.couponService.create(coupon).subscribe(
      (data) => {
        this.loadAllCoupons();
        this.clearCreatingCuopon();
        this.toastr.success('Kupon başarılı oluşturuldu', 'Oluşturma Onay');
      },
      (error) => {
        console.log(error);
        this.toastr.error('Kupon oluşturulurken beklenmedik hata!', 'Oluşturma Hatası');
      }
    );
  }

  deleteCoupon(id: number) {
    this.couponService.delete(id).subscribe(
      () => {
        this.loadAllCoupons();
        this.toastr.success('Kupon başarılı silindi', 'Silme Onay');

      },
      (error) => {
        console.log(error);
        this.toastr.error('Kupon silinirken beklenmedik hata!', 'Silme Hatası');
      }
    );
  }
  openCreateFrom() {
    this.clearAllForm();
    this.isOpenCreateForm = true;
  }
  cancelEdit() {
    this.clearEditingCuopon();
  }
  cancelCreate() {
    this.clearCreatingCuopon();
  }

  clearCreatingCuopon() {
    this.clearAllForm();
    this.creatingCoupon.couponCode = "";
    this.creatingCoupon.couponId = 0;
    this.creatingCoupon.discountAmount = 0;
    this.creatingCoupon.minAmount = 0;
  }
  clearEditingCuopon() {
    this.clearAllForm();
    this.editingCoupon.couponCode = "";
    this.editingCoupon.couponId = 0;
    this.editingCoupon.discountAmount = 0;
    this.editingCoupon.minAmount = 0;
  }
  clearAllForm() {
    this.isOpenCreateForm = false;
    this.isOpenEditForm = false;
    this.isOpenSelectedCoupon = false;
  }
}
