package com.amazon.venezia.command;

import android.os.Binder;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public interface DecisionResult extends IInterface {
    void expire(DecisionExpirationContext decisionExpirationContext) throws RemoteException;

    String getAuthToken() throws RemoteException;

    long getDecisionDurationInSeconds() throws RemoteException;

    String getDescription() throws RemoteException;

    String getDisplayableName() throws RemoteException;

    Map getExtensionData() throws RemoteException;

    Choice getNegativeChoice() throws RemoteException;

    Choice getNeutralChoice() throws RemoteException;

    Choice getPositiveChoice() throws RemoteException;

    public static abstract class Stub extends Binder implements DecisionResult {
        private static final String DESCRIPTOR = "com.amazon.venezia.command.DecisionResult";
        static final int TRANSACTION_expire = 5;
        static final int TRANSACTION_getAuthToken = 1;
        static final int TRANSACTION_getDecisionDurationInSeconds = 4;
        static final int TRANSACTION_getDescription = 3;
        static final int TRANSACTION_getDisplayableName = 2;
        static final int TRANSACTION_getExtensionData = 9;
        static final int TRANSACTION_getNegativeChoice = 8;
        static final int TRANSACTION_getNeutralChoice = 7;
        static final int TRANSACTION_getPositiveChoice = 6;

        public Stub() {
            attachInterface(this, DESCRIPTOR);
        }

        public static DecisionResult asInterface(IBinder obj) {
            if (obj == null) {
                return null;
            }
            IInterface iin = obj.queryLocalInterface(DESCRIPTOR);
            if (iin != null && (iin instanceof DecisionResult)) {
                return (DecisionResult) iin;
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
                    String _result = getAuthToken();
                    reply.writeNoException();
                    reply.writeString(_result);
                    return true;
                case 2:
                    data.enforceInterface(DESCRIPTOR);
                    String _result2 = getDisplayableName();
                    reply.writeNoException();
                    reply.writeString(_result2);
                    return true;
                case 3:
                    data.enforceInterface(DESCRIPTOR);
                    String _result3 = getDescription();
                    reply.writeNoException();
                    reply.writeString(_result3);
                    return true;
                case 4:
                    data.enforceInterface(DESCRIPTOR);
                    long _result4 = getDecisionDurationInSeconds();
                    reply.writeNoException();
                    reply.writeLong(_result4);
                    return true;
                case 5:
                    data.enforceInterface(DESCRIPTOR);
                    DecisionExpirationContext _arg0 = DecisionExpirationContext.Stub.asInterface(data.readStrongBinder());
                    expire(_arg0);
                    reply.writeNoException();
                    return true;
                case 6:
                    data.enforceInterface(DESCRIPTOR);
                    Choice _result5 = getPositiveChoice();
                    reply.writeNoException();
                    reply.writeStrongBinder(_result5 != null ? _result5.asBinder() : null);
                    return true;
                case 7:
                    data.enforceInterface(DESCRIPTOR);
                    Choice _result6 = getNeutralChoice();
                    reply.writeNoException();
                    reply.writeStrongBinder(_result6 != null ? _result6.asBinder() : null);
                    return true;
                case 8:
                    data.enforceInterface(DESCRIPTOR);
                    Choice _result7 = getNegativeChoice();
                    reply.writeNoException();
                    reply.writeStrongBinder(_result7 != null ? _result7.asBinder() : null);
                    return true;
                case 9:
                    data.enforceInterface(DESCRIPTOR);
                    Map _result8 = getExtensionData();
                    reply.writeNoException();
                    reply.writeMap(_result8);
                    return true;
                case 1598968902:
                    reply.writeString(DESCRIPTOR);
                    return true;
                default:
                    return super.onTransact(code, data, reply, flags);
            }
        }

        private static class Proxy implements DecisionResult {
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

            @Override // com.amazon.venezia.command.DecisionResult
            public String getAuthToken() throws RemoteException {
                Parcel _data = Parcel.obtain();
                Parcel _reply = Parcel.obtain();
                try {
                    _data.writeInterfaceToken(Stub.DESCRIPTOR);
                    this.mRemote.transact(1, _data, _reply, 0);
                    _reply.readException();
                    String _result = _reply.readString();
                    return _result;
                } finally {
                    _reply.recycle();
                    _data.recycle();
                }
            }

            @Override // com.amazon.venezia.command.DecisionResult
            public String getDisplayableName() throws RemoteException {
                Parcel _data = Parcel.obtain();
                Parcel _reply = Parcel.obtain();
                try {
                    _data.writeInterfaceToken(Stub.DESCRIPTOR);
                    this.mRemote.transact(2, _data, _reply, 0);
                    _reply.readException();
                    String _result = _reply.readString();
                    return _result;
                } finally {
                    _reply.recycle();
                    _data.recycle();
                }
            }

            @Override // com.amazon.venezia.command.DecisionResult
            public String getDescription() throws RemoteException {
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

            @Override // com.amazon.venezia.command.DecisionResult
            public long getDecisionDurationInSeconds() throws RemoteException {
                Parcel _data = Parcel.obtain();
                Parcel _reply = Parcel.obtain();
                try {
                    _data.writeInterfaceToken(Stub.DESCRIPTOR);
                    this.mRemote.transact(4, _data, _reply, 0);
                    _reply.readException();
                    long _result = _reply.readLong();
                    return _result;
                } finally {
                    _reply.recycle();
                    _data.recycle();
                }
            }

            @Override // com.amazon.venezia.command.DecisionResult
            public void expire(DecisionExpirationContext context) throws RemoteException {
                Parcel _data = Parcel.obtain();
                Parcel _reply = Parcel.obtain();
                try {
                    _data.writeInterfaceToken(Stub.DESCRIPTOR);
                    _data.writeStrongBinder(context != null ? context.asBinder() : null);
                    this.mRemote.transact(5, _data, _reply, 0);
                    _reply.readException();
                } finally {
                    _reply.recycle();
                    _data.recycle();
                }
            }

            @Override // com.amazon.venezia.command.DecisionResult
            public Choice getPositiveChoice() throws RemoteException {
                Parcel _data = Parcel.obtain();
                Parcel _reply = Parcel.obtain();
                try {
                    _data.writeInterfaceToken(Stub.DESCRIPTOR);
                    this.mRemote.transact(6, _data, _reply, 0);
                    _reply.readException();
                    Choice _result = Choice.Stub.asInterface(_reply.readStrongBinder());
                    return _result;
                } finally {
                    _reply.recycle();
                    _data.recycle();
                }
            }

            @Override // com.amazon.venezia.command.DecisionResult
            public Choice getNeutralChoice() throws RemoteException {
                Parcel _data = Parcel.obtain();
                Parcel _reply = Parcel.obtain();
                try {
                    _data.writeInterfaceToken(Stub.DESCRIPTOR);
                    this.mRemote.transact(7, _data, _reply, 0);
                    _reply.readException();
                    Choice _result = Choice.Stub.asInterface(_reply.readStrongBinder());
                    return _result;
                } finally {
                    _reply.recycle();
                    _data.recycle();
                }
            }

            @Override // com.amazon.venezia.command.DecisionResult
            public Choice getNegativeChoice() throws RemoteException {
                Parcel _data = Parcel.obtain();
                Parcel _reply = Parcel.obtain();
                try {
                    _data.writeInterfaceToken(Stub.DESCRIPTOR);
                    this.mRemote.transact(8, _data, _reply, 0);
                    _reply.readException();
                    Choice _result = Choice.Stub.asInterface(_reply.readStrongBinder());
                    return _result;
                } finally {
                    _reply.recycle();
                    _data.recycle();
                }
            }

            @Override // com.amazon.venezia.command.DecisionResult
            public Map getExtensionData() throws RemoteException {
                Parcel _data = Parcel.obtain();
                Parcel _reply = Parcel.obtain();
                try {
                    _data.writeInterfaceToken(Stub.DESCRIPTOR);
                    this.mRemote.transact(9, _data, _reply, 0);
                    _reply.readException();
                    ClassLoader cl = getClass().getClassLoader();
                    Map _result = _reply.readHashMap(cl);
                    return _result;
                } finally {
                    _reply.recycle();
                    _data.recycle();
                }
            }
        }
    }
}
