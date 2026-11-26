package com.amazon.venezia.service.verify;

import android.os.Binder;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public interface IApplicationVerificationService extends IInterface {
    String getAmazonId() throws RemoteException;

    String getDeviceId() throws RemoteException;

    String getToken(String str) throws RemoteException;

    void reportVerificationResults(String str, boolean z, String str2) throws RemoteException;

    public static abstract class Stub extends Binder implements IApplicationVerificationService {
        private static final String DESCRIPTOR = "com.amazon.venezia.service.verify.IApplicationVerificationService";
        static final int TRANSACTION_getAmazonId = 3;
        static final int TRANSACTION_getDeviceId = 4;
        static final int TRANSACTION_getToken = 2;
        static final int TRANSACTION_reportVerificationResults = 1;

        public Stub() {
            attachInterface(this, DESCRIPTOR);
        }

        public static IApplicationVerificationService asInterface(IBinder obj) {
            if (obj == null) {
                return null;
            }
            IInterface iin = obj.queryLocalInterface(DESCRIPTOR);
            if (iin != null && (iin instanceof IApplicationVerificationService)) {
                return (IApplicationVerificationService) iin;
            }
            return new Proxy(obj);
        }

        @Override // android.os.IInterface
        public IBinder asBinder() {
            return this;
        }

        @Override // android.os.Binder
        public boolean onTransact(int code, Parcel data, Parcel reply, int flags) throws RemoteException {
            switch (code) {
                case 1:
                    data.enforceInterface(DESCRIPTOR);
                    String _arg0 = data.readString();
                    boolean _arg1 = data.readInt() != 0;
                    String _arg2 = data.readString();
                    reportVerificationResults(_arg0, _arg1, _arg2);
                    reply.writeNoException();
                    return true;
                case 2:
                    data.enforceInterface(DESCRIPTOR);
                    String _arg3 = data.readString();
                    String _result = getToken(_arg3);
                    reply.writeNoException();
                    reply.writeString(_result);
                    return true;
                case 3:
                    data.enforceInterface(DESCRIPTOR);
                    String _result2 = getAmazonId();
                    reply.writeNoException();
                    reply.writeString(_result2);
                    return true;
                case 4:
                    data.enforceInterface(DESCRIPTOR);
                    String _result3 = getDeviceId();
                    reply.writeNoException();
                    reply.writeString(_result3);
                    return true;
                case 1598968902:
                    reply.writeString(DESCRIPTOR);
                    return true;
                default:
                    return super.onTransact(code, data, reply, flags);
            }
        }

        private static class Proxy implements IApplicationVerificationService {
            private IBinder mRemote;

            Proxy(IBinder remote) {
                this.mRemote = remote;
            }

            @Override // android.os.IInterface
            public IBinder asBinder() {
                return this.mRemote;
            }

            public String getInterfaceDescriptor() {
                return Stub.DESCRIPTOR;
            }

            @Override // com.amazon.venezia.service.verify.IApplicationVerificationService
            public void reportVerificationResults(String packageName, boolean verified, String message) throws RemoteException {
                Parcel _data = Parcel.obtain();
                Parcel _reply = Parcel.obtain();
                try {
                    _data.writeInterfaceToken(Stub.DESCRIPTOR);
                    _data.writeString(packageName);
                    _data.writeInt(verified ? 1 : 0);
                    _data.writeString(message);
                    this.mRemote.transact(1, _data, _reply, 0);
                    _reply.readException();
                } finally {
                    _reply.recycle();
                    _data.recycle();
                }
            }

            @Override // com.amazon.venezia.service.verify.IApplicationVerificationService
            public String getToken(String amazonHeader) throws RemoteException {
                Parcel _data = Parcel.obtain();
                Parcel _reply = Parcel.obtain();
                try {
                    _data.writeInterfaceToken(Stub.DESCRIPTOR);
                    _data.writeString(amazonHeader);
                    this.mRemote.transact(2, _data, _reply, 0);
                    _reply.readException();
                    String _result = _reply.readString();
                    return _result;
                } finally {
                    _reply.recycle();
                    _data.recycle();
                }
            }

            @Override // com.amazon.venezia.service.verify.IApplicationVerificationService
            public String getAmazonId() throws RemoteException {
                Parcel _data = Parcel.obtain();
                Parcel _reply = Parcel.obtain();
                try {
                    _data.writeInterfaceToken(Stub.DESCRIPTOR);
                    this.mRemote.transact(3, _data, _reply, 0);
                    _reply.readException();
                    String _result = _reply.readString();
                    return _result;
                } finally {
                    _reply.recycle();
                    _data.recycle();
                }
            }

            @Override // com.amazon.venezia.service.verify.IApplicationVerificationService
            public String getDeviceId() throws RemoteException {
                Parcel _data = Parcel.obtain();
                Parcel _reply = Parcel.obtain();
                try {
                    _data.writeInterfaceToken(Stub.DESCRIPTOR);
                    this.mRemote.transact(4, _data, _reply, 0);
                    _reply.readException();
                    String _result = _reply.readString();
                    return _result;
                } finally {
                    _reply.recycle();
                    _data.recycle();
                }
            }
        }
    }
}
